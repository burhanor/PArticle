using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Constants
{
	public static class Messages
	{
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
