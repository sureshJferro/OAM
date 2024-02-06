USE [OAM_Dev]

CREATE TABLE [dbo].[api_request_response_log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[request_method] [varchar](10) NULL,
	[request_path] [nvarchar](255) NULL,
	[request_body] [nvarchar](max) NULL,
	[response_status_code] [int] NULL,
	[response_body] [nvarchar](max) NULL,
	[create_time_stamp] [datetime2](7) NULL,
	[update_time_stamp] [datetime2](7) NULL,
	[user_id] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[api_request_response_log] ADD  DEFAULT (getdate()) FOR [create_time_stamp]
GO

ALTER TABLE [dbo].[api_request_response_log] ADD  DEFAULT (getdate()) FOR [update_time_stamp]
GO


CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Created_Time_Stamp] [datetime] NULL,
	[Updated_Time_Stamp] [datetime] NULL,
	[Is_Deleted] [bit] NULL,
	[phone_number] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__PasswordH__37A5467C]  DEFAULT (0x) FOR [PasswordHash]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__PasswordS__38996AB5]  DEFAULT (0x) FOR [PasswordSalt]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__RoleId__398D8EEE]  DEFAULT ((0)) FOR [RoleId]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_CreatedTimeStamp]  DEFAULT (getdate()) FOR [Created_Time_Stamp]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_UpdatedTimeStamp]  DEFAULT (getdate()) FOR [Updated_Time_Stamp]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Is_Deleted]  DEFAULT ((0)) FOR [Is_Deleted]
GO

