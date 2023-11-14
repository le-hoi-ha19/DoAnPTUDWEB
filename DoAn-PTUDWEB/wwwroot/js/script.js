// some scripts

// jquery ready start
$(document).ready(function() {
	// jQuery code

  // var html_download = '<a href="http://bootstrap-ecommerce.com/templates.html" class="btn btn-dark rounded-pill" style="font-size:13px; z-index:100; position: fixed; bottom:10px; right:10px;">Download theme</a>';
  //  $('body').prepend(html_download);
    

	//////////////////////// Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
      e.stopPropagation();
    });


    

	//////////////////////// Bootstrap tooltip
	if($('[data-toggle="tooltip"]').length>0) {  // check if element exists
		$('[data-toggle="tooltip"]').tooltip()
	} // end if




    
}); 
// jquery end

// js phần bình luận
// JavaScript để ẩn/hiển thị các bình luận và kích hoạt nút active
function showComments(commentType) {
    var commentsContainer = document.getElementById('comments-container');
    var commentsDivs = document.querySelectorAll('.comments');

    commentsDivs.forEach(function (div) {
        div.style.display = 'none';
    });

    var selectedCommentsDiv = document.querySelector('.' + commentType);
    if (selectedCommentsDiv) {
        selectedCommentsDiv.style.display = 'block';
    }

    setActiveButton(commentType);
}

// Hàm để kích hoạt nút và thay đổi màu
function setActiveButton(buttonId) {
    var buttons = document.querySelectorAll('button');
    buttons.forEach(function (button) {
        button.classList.remove('active');
    });

    var activeButton = document.querySelector('button[onclick="showComments(\'' + buttonId + '\')"]');
    if (activeButton) {
        activeButton.classList.add('active');
    }
}

// Kích hoạt nút "Tất cả đánh giá" khi trang được tải
window.onload = function () {
    // Hiển thị bình luận tất cả khi trang được tải
    showComments('comments-all');

    // Kích hoạt nút "Tất cả" và đặt màu hồng
    var allCommentsButton = document.querySelector('button[onclick="showComments(\'comments-all\')"]');
    if (allCommentsButton) {
        allCommentsButton.classList.add('active');
    }
};


