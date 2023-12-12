//document.addEventListener('DOMContentLoaded', function () {
//	const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

//	// Kiểm tra xem đã có trạng thái active trong localStorage chưa
//	const activeMenuItemIndex = localStorage.getItem('activeMenuItemIndex');

//	if (activeMenuItemIndex !== null) {
//		// Loại bỏ trạng thái active của tất cả các menu item
//		allSideMenu.forEach(item => {
//			item.parentElement.classList.remove('active');
//		});

//		// Thêm trạng thái active cho menu item được lưu trong localStorage
//		allSideMenu[activeMenuItemIndex].parentElement.classList.add('active');
//	} else {
//		// Nếu không có giá trị trong localStorage, mặc định kích hoạt phần tử đầu tiên
//		allSideMenu[0].parentElement.classList.add('active');
//	}

//	allSideMenu.forEach((item, index) => {
//		const li = item.parentElement;

//		item.addEventListener('click', function () {
//			allSideMenu.forEach((i, idx) => {
//				i.parentElement.classList.remove('active');
//			});
//			li.classList.add('active');

//			// Lưu trạng thái active vào localStorage
//			localStorage.setItem('activeMenuItemIndex', index.toString());
//		});
//	});
//});




// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');

menuBar.addEventListener('click', function () {
	sidebar.classList.toggle('hide');
})







const searchButton = document.querySelector('#content nav form .form-input button');
const searchButtonIcon = document.querySelector('#content nav form .form-input button .bx');
const searchForm = document.querySelector('#content nav form');

searchButton.addEventListener('click', function (e) {
	if(window.innerWidth < 576) {
		e.preventDefault();
		searchForm.classList.toggle('show');
		if(searchForm.classList.contains('show')) {
			searchButtonIcon.classList.replace('bx-search', 'bx-x');
		} else {
			searchButtonIcon.classList.replace('bx-x', 'bx-search');
		}
	}
})





if(window.innerWidth < 768) {
	sidebar.classList.add('hide');
} else if(window.innerWidth > 576) {
	searchButtonIcon.classList.replace('bx-x', 'bx-search');
	searchForm.classList.remove('show');
}


window.addEventListener('resize', function () {
	if(this.innerWidth > 576) {
		searchButtonIcon.classList.replace('bx-x', 'bx-search');
		searchForm.classList.remove('show');
	}
})



const switchMode = document.getElementById('switch-mode');

switchMode.addEventListener('change', function () {
	if(this.checked) {
		document.body.classList.add('dark');
	} else {
		document.body.classList.remove('dark');
	}
})