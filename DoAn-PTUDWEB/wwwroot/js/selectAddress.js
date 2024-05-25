$(document).ready(function () {
    axios.defaults.headers.common['Token'] = token;
    var diachicuthe = $("#diachicuthe");
    var shipAddressElement = $("#shipAddress");
    var address = {
        province: "",
        district: "",
        ward: "",
        diachicuthe:""
    };
    var diachihientai;


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
                    console.log("data>>>", data)
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
        address.district = selectedDistrictName;
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

   


});
