using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Constants
{
	public static class Messages
	{

		public static class Article
		{
			public const string ARTICLE_NOT_FOUND = nameof(ARTICLE_NOT_FOUND);
			public const string ARTICLE_CREATE_SUCCESS = nameof(ARTICLE_CREATE_SUCCESS);
			public const string ARTICLE_CREATE_FAILED = nameof(ARTICLE_CREATE_FAILED);
			public const string ARTICLE_UPDATE_SUCCESS = nameof(ARTICLE_UPDATE_SUCCESS);
			public const string ARTICLE_UPDATE_FAILED = nameof(ARTICLE_UPDATE_FAILED);
			public const string ARTICLE_DELETE_SUCCESS = nameof(ARTICLE_DELETE_SUCCESS);
			public const string ARTICLE_DELETE_FAILED = nameof(ARTICLE_DELETE_FAILED);
			public const string ARTICLE_TITLE_MAX_LENGTH = nameof(ARTICLE_TITLE_MAX_LENGTH);
			public const string ARTICLE_TITLE_REQUIRED = nameof(ARTICLE_TITLE_REQUIRED);
			public const string ARTICLE_SLUG_MAX_LENGTH = nameof(ARTICLE_SLUG_MAX_LENGTH);
			public const string ARTICLE_SLUG_REQUIRED = nameof(ARTICLE_SLUG_REQUIRED);
			public const string ARTICLE_SLUG_ALREADY_EXIST = nameof(ARTICLE_SLUG_ALREADY_EXIST);
			public const string ARTICLE_CONTENT_REQUIRED = nameof(ARTICLE_CONTENT_REQUIRED);

			public const string ARTICLE_STATUS_INVALID = nameof(ARTICLE_STATUS_INVALID);
		}

		public static class ArticleView
		{
			public const string ARTICLE_VIEW_NOT_FOUND = nameof(ARTICLE_VIEW_NOT_FOUND);
			public const string ARTICLE_VIEW_ADDED = nameof(ARTICLE_VIEW_ADDED);
			public const string ARTICLE_VIEW_ADD_ERROR = nameof(ARTICLE_VIEW_ADD_ERROR);
			public const string ARTICLE_VIEW_IP_ADDRESS_REQUIRED = nameof(ARTICLE_VIEW_IP_ADDRESS_REQUIRED);
			public const string ARTICLE_VIEW_ARTICLE_ID_REQUIRED = nameof(ARTICLE_VIEW_ARTICLE_ID_REQUIRED);
			public const string ARTICLE_VIEW_RESET_SUCCESS = nameof(ARTICLE_VIEW_RESET_SUCCESS);
			public const string ARTICLE_VIEW_RESET_FAILED = nameof(ARTICLE_VIEW_RESET_FAILED);
		}
		public static class ArticleVote
		{
			public const string ARTICLE_VOTE_NOT_FOUND = nameof(ARTICLE_VOTE_NOT_FOUND);
			public const string ARTICLE_VOTE_ADDED = nameof(ARTICLE_VOTE_ADDED);
			public const string ARTICLE_VOTE_ADD_ERROR = nameof(ARTICLE_VOTE_ADD_ERROR);
			public const string ARTICLE_VOTE_IP_ADDRESS_REQUIRED = nameof(ARTICLE_VOTE_IP_ADDRESS_REQUIRED);
			public const string ARTICLE_VOTE_ARTICLE_ID_REQUIRED = nameof(ARTICLE_VOTE_ARTICLE_ID_REQUIRED);
			public const string ARTICLE_VOTE_TYPE_INVALID = nameof(ARTICLE_VOTE_TYPE_INVALID);
			public const string ARTICLE_VOTE_ALREADY_EXIST = nameof(ARTICLE_VOTE_ALREADY_EXIST);
			public const string ARTICLE_VOTE_RESET_SUCCESS = nameof(ARTICLE_VOTE_RESET_SUCCESS);
			public const string ARTICLE_VOTE_RESET_FAILED = nameof(ARTICLE_VOTE_RESET_FAILED);
		}
		public static class Auth
		{

			public const string INVALID_EMAIL = nameof(INVALID_EMAIL);
			public const string EMAIL_MAX_LENGTH = nameof(EMAIL_MAX_LENGTH);
			public const string INVALID_NICKNAME = nameof(INVALID_NICKNAME);
			public const string INVALID_NICKNAME_OR_EMAIL = nameof(INVALID_NICKNAME_OR_EMAIL);
			public const string NICKNAME_MAX_LENGTH = nameof(NICKNAME_MAX_LENGTH);
			public const string INVALID_PASSWORD = nameof(INVALID_PASSWORD);
			public const string EMAIL_ALREAY_EXIST = nameof(EMAIL_ALREAY_EXIST);
			public const string NICKNAME_ALREADY_EXIST = nameof(NICKNAME_ALREADY_EXIST);
			public const string REGISTER_SUCCESS = nameof(REGISTER_SUCCESS);
			public const string REGISTER_FAILED = nameof(REGISTER_FAILED);
			public const string LOGIN_SUCCESS = nameof(LOGIN_SUCCESS);
			public const string LOGIN_FAILED = nameof(LOGIN_FAILED);
			public const string LOGOUT_SUCCESS = nameof(LOGOUT_SUCCESS);
			public const string LOGOUT_FAILED = nameof(LOGOUT_FAILED);
		}


		public static class Category
		{
			public const string CATEGORY_NOT_FOUND = nameof(CATEGORY_NOT_FOUND);
			public const string CATEGORY_CREATE_SUCCESS = nameof(CATEGORY_CREATE_SUCCESS);
			public const string CATEGORY_CREATE_FAILED = nameof(CATEGORY_CREATE_FAILED);
			public const string CATEGORY_UPDATE_SUCCESS = nameof(CATEGORY_UPDATE_SUCCESS);
			public const string CATEGORY_UPDATE_FAILED = nameof(CATEGORY_UPDATE_FAILED);
			public const string CATEGORY_DELETE_SUCCESS = nameof(CATEGORY_DELETE_SUCCESS);
			public const string CATEGORY_DELETE_FAILED = nameof(CATEGORY_DELETE_FAILED);
			public const string CATEGORY_NAME_MAX_LENGTH = nameof(CATEGORY_NAME_MAX_LENGTH);
			public const string CATEGORY_NAME_REQUIRED = nameof(CATEGORY_NAME_REQUIRED);
			public const string CATEGORY_SLUG_MAX_LENGTH = nameof(CATEGORY_SLUG_MAX_LENGTH);
			public const string CATEGORY_SLUG_REQUIRED = nameof(CATEGORY_SLUG_REQUIRED);
			public const string CATEGORY_SLUG_ALREADY_EXIST = nameof(CATEGORY_SLUG_ALREADY_EXIST);
			public const string CATEGORY_SLUG_INVALID = nameof(CATEGORY_SLUG_INVALID);

			public const string CATEGORY_STATUS_INVALID = nameof(CATEGORY_STATUS_INVALID);	
			public const string CATEGORY_NAME_ALREADY_EXIST = nameof(CATEGORY_NAME_ALREADY_EXIST);

		}
		public static class Tag
		{
			public const string TAG_NOT_FOUND = nameof(TAG_NOT_FOUND);
			public const string TAG_CREATE_SUCCESS = nameof(TAG_CREATE_SUCCESS);
			public const string TAG_CREATE_FAILED = nameof(TAG_CREATE_FAILED);
			public const string TAG_UPDATE_SUCCESS = nameof(TAG_UPDATE_SUCCESS);
			public const string TAG_UPDATE_FAILED = nameof(TAG_UPDATE_FAILED);
			public const string TAG_DELETE_SUCCESS = nameof(TAG_DELETE_SUCCESS);
			public const string TAG_DELETE_FAILED = nameof(TAG_DELETE_FAILED);
			public const string TAG_NAME_MAX_LENGTH = nameof(TAG_NAME_MAX_LENGTH);
			public const string TAG_NAME_REQUIRED = nameof(TAG_NAME_REQUIRED);
			public const string TAG_SLUG_MAX_LENGTH = nameof(TAG_SLUG_MAX_LENGTH);
			public const string TAG_SLUG_REQUIRED = nameof(TAG_SLUG_REQUIRED);
			public const string TAG_SLUG_ALREADY_EXIST = nameof(TAG_SLUG_ALREADY_EXIST);
			public const string TAG_SLUG_INVALID = nameof(TAG_SLUG_INVALID);
			public const string TAG_STATUS_INVALID = nameof(TAG_STATUS_INVALID);
			public const string TAG_NAME_ALREADY_EXIST = nameof(TAG_NAME_ALREADY_EXIST);
		}
		
		public static class User
		{
			public const string USER_NOT_FOUND = nameof(USER_NOT_FOUND);
			public const string USER_UPDATE_FAILED = nameof(USER_UPDATE_FAILED);
			public const string USER_UPDATE_SUCCESS = nameof(USER_UPDATE_SUCCESS);
			public const string PASSWORD_CHANGED = nameof(PASSWORD_CHANGED);
			public const string INVALID_EMAIL = nameof(INVALID_EMAIL);
			public const string EMAIL_MAX_LENGTH = nameof(EMAIL_MAX_LENGTH);
			public const string INVALID_NICKNAME = nameof(INVALID_NICKNAME);
			public const string NICKNAME_MAX_LENGTH = nameof(NICKNAME_MAX_LENGTH);
			public const string INVALID_PASSWORD = nameof(INVALID_PASSWORD);
			public const string EMAIL_ALREAY_EXIST = nameof(EMAIL_ALREAY_EXIST);
			public const string NICKNAME_ALREADY_EXIST = nameof(NICKNAME_ALREADY_EXIST);
			public const string OLD_PASSWORD_WRONG = nameof(OLD_PASSWORD_WRONG);
			public const string PASSWORD_UNMATCHED = nameof(PASSWORD_UNMATCHED);
		}
	}
}
