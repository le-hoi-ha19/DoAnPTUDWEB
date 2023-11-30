using DoAn_PTUDWEB.Models;
using Microsoft.CodeAnalysis;

namespace DoAn_PTUDWEB.Services
{
	public class ReviewService
	{
        private readonly DataContext _context;

		public ReviewService(DataContext context) 
		{ 
            _context = context;
		}
		// GET: AllReview
		public List<ReviewDetail> GetAllReview(int id)
		{
			var ListComment = (from User in _context.TbUsers
							  join review in _context.TbReviews on User.UserId equals review.UserId
							  where review.ProductId == id
							  select new ReviewDetail
							  {
								  FullName = User.FullName,
								  Rating = review.Rating,
								  Comment = review.Comment,
								  CreatedDate = review.CreatedDate,
							  }).Take(6).ToList();
			return ListComment;
		}

		// GET: ReviewBYId
		public List<ReviewDetail> GetReviewByStar(int id,int? rating)
		{
			var ListCommentByStar = (from User in _context.TbUsers
									 join review in _context.TbReviews on User.UserId equals review.UserId
									 where review.ProductId == id && review.Rating == rating
									 select new ReviewDetail
									 {
										 FullName = User.FullName,
										 Rating = review.Rating,
										 Comment = review.Comment,
										 CreatedDate = review.CreatedDate,
									 }).Take(6).ToList();

			return ListCommentByStar;
		}
		// Post: Review
		public void CreateReview(TbReview review)
		{

		}
	}
}
