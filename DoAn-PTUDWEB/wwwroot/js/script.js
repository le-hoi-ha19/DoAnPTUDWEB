
// đoạn mã bên trong sẽ được thực hiện khi dom load xong
$(document).ready(function () {
	
    // Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
      e.stopPropagation();
    });

	// Bootstrap tooltip
	if($('[data-toggle="tooltip"]').length>0) {  // check if element exists
		$('[data-toggle="tooltip"]').tooltip()
	} // end if

// Thay đổi ảnh to khi hover chuột vào ảnh nhỏ
    var bigImage = document.querySelector(".bigImage");
    var smallImages = document.querySelectorAll('.smallImage');
    smallImages.forEach(function (smallImage) {
        smallImage.addEventListener("mouseenter", function (e) {
            bigImage.src = e.target.src;
        });
    });


    //js phần cho mấy sao của dialog đánh giá
    // Select all elements with the "i" tag and store them in a NodeList called "stars"
    const stars = document.querySelectorAll(".stars i");

    // Loop through the "stars" NodeList
    stars.forEach((star, index1) => {
        // Add an event listener that runs a function when the "click" event is triggered
        star.addEventListener("click", () => {
            // Loop through the "stars" NodeList Again
            stars.forEach((star, index2) => {
                // Add the "active" class to the clicked star and any stars with a lower index
                // and remove the "active" class from any stars with a higher index
                index1 >= index2 ? star.classList.add("active") : star.classList.remove("active");
            });
        });
    });


    //js phần ẩn hiện form đánh giá
    var btnOpen = document.querySelector('#openDialog');
    var dialog = document.querySelector('#dialog');
    var btnClose = document.querySelector('.closeDialog');
    btnOpen.addEventListener('click', function () {
        // Thay đổi style để hiển thị
        dialog.style.display = "block";
    });

    btnClose.addEventListener('click', function () {
       
        // Thay đổi style để đóng
        dialog.style.display = "none";
    });




    // hàm tạp ra icon sao với số rating tương ứng
    function generateStarIcons(rating) {
        // Create an array of empty strings repeated based on the rating
        const starIcons = Array.from({ length: rating }, () => '<li>&#9733;</li>');

        // Join the array into a single string
        return starIcons.join('');
    }



    var ProductId = document.querySelector('.Product-Name').getAttribute('data-id')

    // Lắng nghe sự kiện click cho nút "Tất cả"
    $("#btnAll").click(function () {
        $(this).addClass("border-primary");
        
        $("#btn5Star, #btn4Star, #btn3Star, #btn2Star, #btn1Star").removeClass("border-primary");
        callApi(ProductId,null); // rating = null
    });

    // Lắng nghe sự kiện click cho các nút đánh giá từ 1 đến 5 sao
    for (let i = 1; i <= 5; i++) {
        $(`#btn${i}Star`).click(function () {
            $(this).addClass("border-primary");
            // Bỏ lớp border-primary từ nút "Tất cả"
            $("#btnAll").removeClass("border-primary");
            // Bỏ lớp border-primary từ tất cả các nút đánh giá khác
            $(`#btn5Star, #btn4Star, #btn3Star, #btn2Star, #btn1Star`).not(this).removeClass("border-primary");
            callApi(ProductId,i);
        });
    }

    // hàm goị api để xem từng loại đánh giá theo số sao
    function callApi(ProductId,rating) {
        var reviewcomment = document.querySelector('.reviewcomment');

        // Xóa nội dung bình luận trước đó
        reviewcomment.innerHTML = ""; 

        // Gọi API bằng fetch 
        fetch(`/Product/Reviews?ProductId=${ProductId}&rating=${rating}`)
            .then(response => response.json())
            .then(data => {
                // Xử lý dữ liệu nhận được từ API
                console.log(data);

                if (data.length > 0) {
                    data.forEach((review) => {
                        // Tạo một phần tử mới để hiển thị bình luận và hình ảnh
                        var newComment = document.createElement('div');
                        newComment.innerHTML = `
                                    <div class="border-bottom mb-5">
										<div class="w-50 d-flex">
											<img class="rounded-circle img-sm border mr-3 " src="/images/avatars/avatar3.jpg">

											 <div>
												<span class="UserName-review">${review.fullName}</span>
												<div>
													<ul class="rating-stars-review orange-stars" >
													    ${generateStarIcons(review.rating)}
													</ul>
											    </div>
											<p class="createDate-review">${review.createdDate} </p>

										    </div>
									    </div>

										<p class="my-3 content-review">
											${review.comment}
										</p>
									</div>
                    `;

                        // Thêm phần tử mới vào reviewcomment
                        reviewcomment.appendChild(newComment);
                    });
                } else {
                    var noComment = document.createElement('div');
                    noComment.innerHTML = `<p> không có đánh giá nào</p> `
                    reviewcomment.appendChild(noComment);
                }
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });
    }


    

}); 








