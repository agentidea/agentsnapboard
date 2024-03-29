USE [AgentStory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SposorSelf]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_SposorSelf]
GO
USE [AgentStory]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/29/2007 21:54:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]


USE [AgentStory]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/29/2007 21:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[username] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[password] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[nick] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[email] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[roles] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[state] [tinyint] NOT NULL,
	[OrigInvitationCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[dateActivated] [datetime] NULL,
	[dateLastActive] [datetime] NULL,
	[pendingGUID] [uniqueidentifier] NULL,
	[userGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Users_userGUID]  DEFAULT (newid()),
	[sponsorID] [int] NULL,
	[notificationFrequency] [int] NOT NULL,
	[notificationTypes] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[tags] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OrigInviteCode] UNIQUE NONCLUSTERED 
(
	[OrigInvitationCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [AgentStory]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_SposorSelf] FOREIGN KEY([sponsorID])
REFERENCES [dbo].[Users] ([id])