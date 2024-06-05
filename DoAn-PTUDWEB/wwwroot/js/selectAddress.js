$(document).ready(function () {
    var token = 'a2b5cb39-19e4-11ef-9d18-eec40c4c1a75';
    axios.defaults.headers.common['Token'] = token;
    var diachicuthe = $("#diachicuthe");
    var shipAddressElement = $("#shipAddress");
    var address = {
        province: "",
        district: "",
        districtId: 0,
        ward: "",
        wardId: 0,
        diachicuthe:""
    };
    var diachihientai;
    var totalMoneyElement = $("#totalMoney");
    var totalMoneyText = totalMoneyElement.text();
    var totalMoneyValue = totalMoneyText.replace('Tổng tiền:', '').replace(' đ', '').trim();
    var totalMoneyNumber = parseFloat(totalMoneyValue.replace(/,/g, ''));
    var tempSelectedService_id;



  

    //lấy danh sách tỉnh/thành phố
    axios.get('https://online-gateway.ghn.vn/shiip/public-api/master-data/province')
        .then(function (response) {
            var data = response.data.data;
            var $select = $('#provinceSelect');
            $select.empty();
            // Thêm option mặc định
            $select.append('<option value="" selected>Vui lòng Chọn Tỉnh/Thành phố</option>');
            // Đổ dữ liệu vào thẻ select
            $.each(data, function (index, item) {
                $select.append('<option  value="' + item.ProvinceID + '">' + item.ProvinceName + '</option>');
            });
            // Bắt sự kiện onchange cho thẻ select
            $select.on('change', function () {
                handleSelectedProvince(this);
            });
        })
        .catch(function (error) {
            console.error('Có lỗi xảy ra khi gọi API:', error);
        });



    const handleSelectedProvince = (selectElement) => {
        var selectedProvinceId = $(selectElement).val();
        var selectedProvinceName = $(selectElement).find('option:selected').text();
        address.province = selectedProvinceName;
        
        if (selectedProvinceId) {
            axios({
                method: 'get',
                url: 'https://online-gateway.ghn.vn/shiip/public-api/master-data/district',
                params: {
                    province_id: selectedProvinceId
                },
                headers: { token: token },
            })
                .then(function (response) {
                    var data = response.data.data; // Lưu ý rằng dữ liệu thường được lồng trong một field data
                    var $select = $('#districtSelect');
                    $select.empty();
                    // Thêm option mặc định
                    $select.append('<option value="" selected>Vui lòng Chọn Quận/Huyện</option>');
                    // Đổ dữ liệu vào thẻ select
                    $.each(data, function (index, item) {
                        $select.append('<option value="' + item.DistrictID + '">' + item.DistrictName + '</option>');
                    });
                    $select.on('change', function () {
                        handleSelectedDistrict(this);
                    });
                })
                .catch(function (error) {
                    console.error('Có lỗi xảy ra khi gọi API:', error);
                });

        }


    }
    const handleSelectedDistrict = (e) => {
        var selectedDistrictID = $(e).val();
        var selectedDistrictName = $(e).find('option:selected').text();
        console.log("selectedDistrictID>>>>", selectedDistrictID)
        address.district = selectedDistrictName;
        address.districtId = selectedDistrictID;
        if (selectedDistrictID) {
            axios({
                method: 'get',
                url: 'https://online-gateway.ghn.vn/shiip/public-api/master-data/ward',
                params: {
                    district_id: selectedDistrictID,
                },
                headers: { token: token },
            })
                .then(function (response) {
                    var data = response.data.data; // Lưu ý rằng dữ liệu thường được lồng trong một field data
                    var $select = $('#wardSelect');
                    $select.empty();
                    // Thêm option mặc định
                    $select.append('<option value="" selected>Vui lòng Chọn Quận/Huyện</option>');
                    // Đổ dữ liệu vào thẻ select
                    $.each(data, function (index, item) {
                        $select.append('<option value="' + item.WardCode + '">' + item.WardName + '</option>');
                    });
                    $select.on('change', function () {
                        handleSelectedWard(this);
                    });
                })
                .catch(function (error) {
                    console.error('Có lỗi xảy ra khi gọi API:', error);
                });
        }

    }

    const handleSelectedWard = (value) => {
        var selectedWardID = $(value).val();
        var selectedWardName = $(value).find('option:selected').text();
        address.ward = selectedWardName;
        address.wardId = selectedWardID;
        handleCaculateFee();
    }

    diachicuthe.on("input", () => {
        var temp = diachicuthe.val();
        if (temp) {
            address.diachicuthe = temp;
        }

        // Kiểm tra xem các thuộc tính khác của đối tượng address đã được cập nhật chưa
        if (address.ward && address.district && address.province) {
            diachihientai = `${address.diachicuthe}, ${address.ward}, ${address.district}, ${address.province}`;
            shipAddressElement.val(diachihientai);
        }
    });

  


    const handleCaculateFee = () => {
        var selectedService_id = 53322;
        axios({
            method: 'get',
            url: ' https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee',
            params: {
                service_id: selectedService_id,
                "insurance_value": totalMoneyNumber,
                "coupon": null,
                "from_district_id": 1849,
                "to_district_id": address.districtId,
                "to_ward_code": address.wardId.toString(),
                "height": 15,
                "length": 15,
                "weight": 500,
                "width": 15
            },
            headers: { token: token, shop_id: 5087531, },
        })
            .then(function (response) {
                var data = response.data.data; // Lưu ý rằng dữ liệu thường được lồng trong một field data
                console.log("phí giao hàng", data)
                $("#feeInsurrent").html(data.insurance_fee); 
                $("#feeShip").html(data.service_fee); 
                calculateTotal();
            })
            .catch(function (error) {
                console.error('Có lỗi xảy ra khi gọi API:', error);
            });
    }   


    // Function to calculate the total amount
    function calculateTotal() {
        // Get the values of "Tiền hàng", "Phí bảo hiểm", and "Phí vận chuyển" spans
        var totalMoney = parseFloat($("#totalMoney").text());
        var feeInsurrent = parseFloat($("#feeInsurrent").text());
        var feeShip = parseFloat($("#feeShip").text());

        // Calculate the total amount
        var totalPrice = totalMoney + feeInsurrent + feeShip;

        // Convert totalPrice to string and remove trailing zeros
        var totalPriceString = totalPrice.toFixed(4).replace(/\.?0+$/, '');

        // Update the "Tổng tiền" span with the calculated total amount
        $("#tongtien").text(totalPriceString );
    }
});
