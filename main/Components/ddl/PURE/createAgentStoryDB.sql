USE [master]
GO
/****** Object:  Database [AgentStory]    Script Date: 03/05/2008 06:31:52 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'AgentStory')
BEGIN
CREATE DATABASE [AgentStory] ON  PRIMARY 
( NAME = N'AgentStory', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\AgentStory.mdf' , SIZE = 13504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AgentStory_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\AgentStory_log.LDF' , SIZE = 504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
END

GO
EXEC dbo.sp_dbcmptlevel @dbname=N'AgentStory', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgentStory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgentStory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgentStory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgentStory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgentStory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgentStory] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgentStory] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AgentStory] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AgentStory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgentStory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgentStory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgentStory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgentStory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgentStory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgentStory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgentStory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgentStory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgentStory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgentStory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgentStory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgentStory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgentStory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgentStory] SET  READ_WRITE 
GO
ALTER DATABASE [AgentStory] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgentStory] SET  MULTI_USER 
GO
ALTER DATABASE [AgentStory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgentStory] SET DB_CHAINING OFF 
USE [AgentStory]
GO
/****** Object:  Table [dbo].[PageElementType]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementType](
	[id] [int] NOT NULL,
	[code] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_PageElementType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Words]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Words]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Words](
	[id] [int] NOT NULL,
	[word] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NULL,
	[nick] [nvarchar](50) NULL,
	[email] [nvarchar](128) NULL,
	[roles] [nvarchar](50) NULL,
	[state] [tinyint] NOT NULL,
	[OrigInvitationCode] [nvarchar](50) NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[dateActivated] [datetime] NULL,
	[dateLastActive] [datetime] NULL,
	[pendingGUID] [uniqueidentifier] NULL,
	[userGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Users_userGUID]  DEFAULT (newid()),
	[sponsorID] [int] NULL,
	[notificationFrequency] [int] NOT NULL,
	[notificationTypes] [nvarchar](128) NOT NULL,
	[tags] [ntext] NULL,
	[properties] [ntext] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OrigInviteCode] UNIQUE NONCLUSTERED 
(
	[OrigInvitationCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryState]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryState](
	[id] [int] NOT NULL,
	[stateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StoryState] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryViewElement]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryViewElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryViewElement](
	[story_view_id] [int] NOT NULL,
	[element_id] [int] NOT NULL,
	[sequence_number] [int] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msg] [ntext] NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[vStoryPlatform]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vStoryPlatform]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vStoryPlatform]
AS
SELECT     dbo.Story.title, dbo.FACT_StoryView.rating, dbo.FACT_StoryView.views, dbo.FACT_StoryView.lastEditedBy, dbo.FACT_StoryView.lastEditedWhen, 
                      dbo.FACT_StoryView.story_id, dbo.Story.state
FROM         dbo.FACT_StoryView INNER JOIN
                      dbo.Story ON dbo.FACT_StoryView.story_id = dbo.Story.id
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[4] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "FACT_StoryView"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 193
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Story"
            Begin Extent = 
               Top = 6
               Left = 231
               Bottom = 114
               Right = 398
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPlatform'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPlatform'

GO
/****** Object:  View [dbo].[vEmailMessage]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vEmailMessage]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vEmailMessage]
AS
SELECT     dbo.emailMessage.id, dbo.emailMessage.subject, dbo.emailMessage.[to] AS ''toAddress'', dbo.emailMessage.[from] AS ''fromAddress'', 
                      dbo.emailMessage.body, dbo.emailMessageStates.name AS ''stateHR'', dbo.emailMessage.state, dbo.emailMessage.dateAdded, 
                      dbo.emailMessage.userAddedID, dbo.emailMessage.guid, dbo.emailMessage.reply_to, dbo.emailMessage.lastModified, 
                      dbo.Users.firstName AS ''toFirstName'', dbo.Users.lastName AS ''toLastName'', dbo.Users.email, dbo.Users.OrigInvitationCode
FROM         dbo.emailMessage INNER JOIN
                      dbo.emailMessageStates ON dbo.emailMessage.state = dbo.emailMessageStates.id INNER JOIN
                      dbo.Users ON dbo.emailMessage.userAddedID = dbo.Users.id
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "emailMessage"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 289
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "emailMessageStates"
            Begin Extent = 
               Top = 152
               Left = 235
               Bottom = 230
               Right = 386
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 75
               Left = 519
               Bottom = 340
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vEmailMessage'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vEmailMessage'

GO
/****** Object:  Table [dbo].[emailMessageStates]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessageStates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[emailMessageStates](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_emailMessageStates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FACT_StoryView]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryView]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FACT_StoryView](
	[story_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[views] [int] NOT NULL,
	[lastEditedBy] [int] NULL,
	[lastEditedWhen] [datetime] NULL,
 CONSTRAINT [PK_FACT_StoryView] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[groupStartedBy] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[description] [ntext] NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PagePageElementMap]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PagePageElementMap](
	[page_id] [int] NOT NULL,
	[pageElementMap_id] [int] NOT NULL,
	[sequence] [int] NULL,
 CONSTRAINT [PK_PagePageElementMap] PRIMARY KEY CLUSTERED 
(
	[page_id] ASC,
	[pageElementMap_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageElement]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NULL,
	[typeID] [int] NOT NULL,
	[value] [text] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[tags] [nvarchar](50) NULL,
	[user_id_originator] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[properties] [ntext] NULL,
	[lastModified] [datetime] NULL,
	[lastModifiedBy] [int] NULL,
 CONSTRAINT [PK_PageElement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FACT_StoryUserView]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FACT_StoryUserView](
	[user_id] [int] NOT NULL,
	[story_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[views] [int] NOT NULL,
 CONSTRAINT [PK_StoryView] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[story_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryTxLog]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryTxLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryTxLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[story_ID] [int] NOT NULL,
	[command] [ntext] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[user_ID] [int] NOT NULL,
	[seq] [int] NOT NULL,
 CONSTRAINT [PK_StoryTxLog] PRIMARY KEY CLUSTERED 
(
	[story_ID] ASC,
	[seq] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

/****** Object:  Index [IX_StoryTxLog]    Script Date: 03/05/2008 06:31:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StoryTxLog]') AND name = N'IX_StoryTxLog')
CREATE NONCLUSTERED INDEX [IX_StoryTxLog] ON [dbo].[StoryTxLog] 
(
	[story_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoryChangeLog]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryChangeLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryChangeLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[story_id] [int] NOT NULL,
	[seq] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[changeEvent] [ntext] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[priority] [int] NOT NULL,
 CONSTRAINT [PK_StoryChangeLog] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[seq] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

/****** Object:  Index [IX_StoryChangeLog]    Script Date: 03/05/2008 06:31:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StoryChangeLog]') AND name = N'IX_StoryChangeLog')
CREATE NONCLUSTERED INDEX [IX_StoryChangeLog] ON [dbo].[StoryChangeLog] 
(
	[story_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoryGroupEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryGroupEditors](
	[story_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryGroupEditors] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryGroupViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryGroupViewers](
	[story_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryGroupViewers] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryPage]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryPage](
	[story_id] [int] NOT NULL,
	[page_id] [int] NOT NULL,
 CONSTRAINT [PK_StoryPage] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[page_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryPageElement]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryPageElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryPageElement](
	[story_id] [int] NOT NULL,
	[pageElement_id] [int] NOT NULL,
 CONSTRAINT [PK_StoryPageElement] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[pageElement_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryUserEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryUserEditors](
	[story_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryUserEditors] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoryUserViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoryUserViewers](
	[story_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StoryUserViewers] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageElementUserViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementUserViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementUserViewers](
	[peID] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageElementUserViewers] PRIMARY KEY CLUSTERED 
(
	[peID] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageElementUserEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementUserEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementUserEditors](
	[peID] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageElementUserEditors] PRIMARY KEY CLUSTERED 
(
	[peID] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UsersGroups]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersGroups](
	[user_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_UsersGroups] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Story]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Story]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Story](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[user_id_originator] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Story_guid]  DEFAULT (newid()),
	[viewableBy] [nvarchar](255) NOT NULL,
	[editableBy] [nvarchar](255) NOT NULL,
	[description] [ntext] NULL,
	[state] [int] NOT NULL CONSTRAINT [DF_Story_state]  DEFAULT ((0)),
	[typeStory] [int] NULL,
	[properties] [ntext] NULL,
 CONSTRAINT [PK_Story] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Page]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Page](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NULL,
	[name] [nvarchar](255) NOT NULL,
	[seq] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[gridX] [int] NOT NULL,
	[gridY] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[user_id_originator] [int] NOT NULL,
	[gridZ] [int] NOT NULL,
	[style] [int] NULL,
	[properties] [ntext] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageUserViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageUserViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageUserViewers](
	[pageID] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageUserViewers] PRIMARY KEY CLUSTERED 
(
	[pageID] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageUserEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageUserEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageUserEditors](
	[pageID] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageUserEditors] PRIMARY KEY CLUSTERED 
(
	[pageID] ASC,
	[user_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StrategyTable]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StrategyTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StrategyTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[stJSON64] [ntext] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateLastModified] [datetime] NOT NULL,
	[owner_id] [int] NOT NULL,
	[name64] [nvarchar](255) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StrategyTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

/****** Object:  Index [IX_StrategyTable]    Script Date: 03/05/2008 06:31:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StrategyTable]') AND name = N'IX_StrategyTable')
CREATE UNIQUE NONCLUSTERED INDEX [IX_StrategyTable] ON [dbo].[StrategyTable] 
(
	[GUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invitation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Invitation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[viewURL] [nvarchar](255) NULL,
	[imgURL] [nvarchar](255) NULL,
	[invitationCode] [nvarchar](50) NOT NULL,
	[invitationText] [text] NULL,
	[user_id_to] [int] NOT NULL,
	[user_id_from] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Invitation_guid]  DEFAULT (newid()),
	[title] [nvarchar](255) NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[InviteEvent] [nvarchar](50) NOT NULL,
	[lastError] [ntext] NULL,
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageGroupViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageGroupViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageGroupViewers](
	[pageID] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageGroupViewers] PRIMARY KEY CLUSTERED 
(
	[pageID] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageGroupEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageGroupEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageGroupEditors](
	[pageID] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageGroupEditors] PRIMARY KEY CLUSTERED 
(
	[pageID] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageElementMap]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementMap]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementMap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NULL,
	[pageElement_id] [int] NOT NULL,
	[gridX] [int] NOT NULL,
	[gridY] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[gridZ] [int] NULL,
	[user_id_originator] [int] NULL,
	[lastModified] [datetime] NULL,
 CONSTRAINT [PK_PageElementMap] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Index [IX_PageElementMap]    Script Date: 03/05/2008 06:31:57 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PageElementMap]') AND name = N'IX_PageElementMap')
CREATE UNIQUE NONCLUSTERED INDEX [IX_PageElementMap] ON [dbo].[PageElementMap] 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageElementGroupEditors]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementGroupEditors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementGroupEditors](
	[peID] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageElementGroupEditors] PRIMARY KEY CLUSTERED 
(
	[peID] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageElementGroupViewers]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageElementGroupViewers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageElementGroupViewers](
	[peID] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_PageElementGroupViewers] PRIMARY KEY CLUSTERED 
(
	[peID] ASC,
	[group_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[emailMessage]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[emailMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[emailMessage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject] [nvarchar](255) NOT NULL,
	[to] [nvarchar](128) NOT NULL,
	[from] [nvarchar](128) NOT NULL,
	[body] [ntext] NOT NULL,
	[state] [int] NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[userAddedID] [int] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[reply_to] [nvarchar](128) NOT NULL,
	[lastModified] [datetime] NOT NULL,
	[lastError] [ntext] NULL,
 CONSTRAINT [PK_emailMessage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[vStoryPageLibrary]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vStoryPageLibrary]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vStoryPageLibrary]
AS
SELECT     TOP (100) PERCENT dbo.PageElementMap.pageElement_id, dbo.PageElementMap.gridX, dbo.PageElementMap.gridY, dbo.PageElementMap.gridZ, 
                      dbo.Page.name AS PageName, dbo.Page.id AS Page_ID, dbo.Story.id AS Story_ID, dbo.PageElementMap.guid AS PEM_GUID, 
                      dbo.PageElementMap.dateAdded, dbo.Page.seq AS PageSeq
FROM         dbo.Page INNER JOIN
                      dbo.PagePageElementMap ON dbo.Page.id = dbo.PagePageElementMap.page_id INNER JOIN
                      dbo.PageElementMap ON dbo.PagePageElementMap.pageElementMap_id = dbo.PageElementMap.id INNER JOIN
                      dbo.StoryPage ON dbo.Page.id = dbo.StoryPage.page_id INNER JOIN
                      dbo.Story ON dbo.StoryPage.story_id = dbo.Story.id
ORDER BY dbo.StoryPage.story_id, dbo.PageElementMap.pageElement_id
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Page"
            Begin Extent = 
               Top = 42
               Left = 160
               Bottom = 150
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PagePageElementMap"
            Begin Extent = 
               Top = 42
               Left = 452
               Bottom = 135
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PageElementMap"
            Begin Extent = 
               Top = 10
               Left = 731
               Bottom = 290
               Right = 898
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StoryPage"
            Begin Extent = 
               Top = 233
               Left = 24
               Bottom = 311
               Right = 175
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Story"
            Begin Extent = 
               Top = 235
               Left = 266
               Bottom = 343
               Right = 433
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Be' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPageLibrary'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'gin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPageLibrary'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryPageLibrary'

GO
/****** Object:  View [dbo].[vStoryLibrary]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vStoryLibrary]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vStoryLibrary]
AS
SELECT     dbo.Story.id AS story_id, dbo.Story.title, dbo.PageElement.value, dbo.PageElement.guid, dbo.PageElement.id
FROM         dbo.PageElement INNER JOIN
                      dbo.StoryPageElement ON dbo.PageElement.id = dbo.StoryPageElement.pageElement_id INNER JOIN
                      dbo.Story ON dbo.StoryPageElement.story_id = dbo.Story.id
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PageElement"
            Begin Extent = 
               Top = 131
               Left = 733
               Bottom = 358
               Right = 900
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Story"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StoryPageElement"
            Begin Extent = 
               Top = 186
               Left = 355
               Bottom = 264
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryLibrary'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vStoryLibrary'

GO
/****** Object:  View [dbo].[vPagesByStory]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vPagesByStory]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vPagesByStory]
AS
SELECT     TOP (100) PERCENT dbo.StoryPage.story_id, dbo.StoryPage.page_id, dbo.Page.seq, dbo.Page.name, dbo.Page.guid, dbo.Page.id
FROM         dbo.Page INNER JOIN
                      dbo.StoryPage ON dbo.Page.id = dbo.StoryPage.page_id
ORDER BY dbo.Page.seq
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Page"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 245
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StoryPage"
            Begin Extent = 
               Top = 6
               Left = 248
               Bottom = 91
               Right = 404
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vPagesByStory'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vPagesByStory'

GO
/****** Object:  View [dbo].[vPageElemByUser]    Script Date: 03/05/2008 06:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vPageElemByUser]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vPageElemByUser]
AS
SELECT     TOP (100) PERCENT dbo.PageElement.id, dbo.PageElement.code, dbo.PageElement.typeID, dbo.PageElement.value, dbo.PageElement.guid, 
                      dbo.PageElement.tags, dbo.PageElement.user_id_originator, dbo.PageElement.dateAdded, dbo.PageElement.properties, 
                      dbo.PageElement.lastModified, dbo.PageElement.lastModifiedBy, dbo.Users.username, dbo.Users.id AS UserOriginator
FROM         dbo.PageElement INNER JOIN
                      dbo.Users ON dbo.PageElement.user_id_originator = dbo.Users.id
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PageElement"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 243
               Bottom = 114
               Right = 427
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vPageElemByUser'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vPageElemByUser'

GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
USE [AgentStory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SposorSelf]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_SposorSelf] FOREIGN KEY([sponsorID])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagePageElementMap_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]'))
ALTER TABLE [dbo].[PagePageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PagePageElementMap_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap] CHECK CONSTRAINT [FK_PagePageElementMap_Page]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagePageElementMap_PageElementMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagePageElementMap]'))
ALTER TABLE [dbo].[PagePageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PagePageElementMap_PageElementMap] FOREIGN KEY([pageElementMap_id])
REFERENCES [dbo].[PageElementMap] ([id])
GO
ALTER TABLE [dbo].[PagePageElementMap] CHECK CONSTRAINT [FK_PagePageElementMap_PageElementMap]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElement_PageElementType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElement]'))
ALTER TABLE [dbo].[PageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElement_PageElementType] FOREIGN KEY([typeID])
REFERENCES [dbo].[PageElementType] ([id])
GO
ALTER TABLE [dbo].[PageElement] CHECK CONSTRAINT [FK_PageElement_PageElementType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElement_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElement]'))
ALTER TABLE [dbo].[PageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElement_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[PageElement] CHECK CONSTRAINT [FK_PageElement_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryView_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[FACT_StoryUserView]'))
ALTER TABLE [dbo].[FACT_StoryUserView]  WITH CHECK ADD  CONSTRAINT [FK_StoryView_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTxLog_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTxLog]'))
ALTER TABLE [dbo].[StoryTxLog]  WITH CHECK ADD  CONSTRAINT [FK_StoryTxLog_Story] FOREIGN KEY([story_ID])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryTxLog_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryTxLog]'))
ALTER TABLE [dbo].[StoryTxLog]  WITH CHECK ADD  CONSTRAINT [FK_StoryTxLog_Users] FOREIGN KEY([user_ID])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryChangeLog_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryChangeLog]'))
ALTER TABLE [dbo].[StoryChangeLog]  WITH CHECK ADD  CONSTRAINT [FK_StoryChangeLog_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryChangeLog_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryChangeLog]'))
ALTER TABLE [dbo].[StoryChangeLog]  WITH CHECK ADD  CONSTRAINT [FK_StoryChangeLog_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupEditors_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupEditors]'))
ALTER TABLE [dbo].[StoryGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryGroupViewers_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryGroupViewers]'))
ALTER TABLE [dbo].[StoryGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryGroupViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPage_Page] FOREIGN KEY([page_id])
REFERENCES [dbo].[Page] ([id])
GO
ALTER TABLE [dbo].[StoryPage] CHECK CONSTRAINT [FK_StoryPage_Page]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPage_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPage]'))
ALTER TABLE [dbo].[StoryPage]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPage_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPage] CHECK CONSTRAINT [FK_StoryPage_Story]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPageElement_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPageElement]'))
ALTER TABLE [dbo].[StoryPageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPageElement_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement] CHECK CONSTRAINT [FK_StoryPageElement_PageElement]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryPageElement_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryPageElement]'))
ALTER TABLE [dbo].[StoryPageElement]  WITH NOCHECK ADD  CONSTRAINT [FK_StoryPageElement_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
ALTER TABLE [dbo].[StoryPageElement] CHECK CONSTRAINT [FK_StoryPageElement_Story]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserEditors_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]'))
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserEditors]'))
ALTER TABLE [dbo].[StoryUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserEditors_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserViewers_Story]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]'))
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Story] FOREIGN KEY([story_id])
REFERENCES [dbo].[Story] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StoryUserViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StoryUserViewers]'))
ALTER TABLE [dbo].[StoryUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_StoryUserViewers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementUserViewers_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementUserViewers]'))
ALTER TABLE [dbo].[PageElementUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageElementUserViewers_PageElement] FOREIGN KEY([peID])
REFERENCES [dbo].[PageElement] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementUserViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementUserViewers]'))
ALTER TABLE [dbo].[PageElementUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageElementUserViewers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementUserEditors_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementUserEditors]'))
ALTER TABLE [dbo].[PageElementUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageElementUserEditors_PageElement] FOREIGN KEY([peID])
REFERENCES [dbo].[PageElement] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementUserEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementUserEditors]'))
ALTER TABLE [dbo].[PageElementUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageElementUserEditors_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersGroups_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersGroups]'))
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersGroups_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersGroups]'))
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Story_StoryState]') AND parent_object_id = OBJECT_ID(N'[dbo].[Story]'))
ALTER TABLE [dbo].[Story]  WITH NOCHECK ADD  CONSTRAINT [FK_Story_StoryState] FOREIGN KEY([state])
REFERENCES [dbo].[StoryState] ([id])
GO
ALTER TABLE [dbo].[Story] CHECK CONSTRAINT [FK_Story_StoryState]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Story_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Story]'))
ALTER TABLE [dbo].[Story]  WITH NOCHECK ADD  CONSTRAINT [FK_Story_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Story] CHECK CONSTRAINT [FK_Story_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Page]'))
ALTER TABLE [dbo].[Page]  WITH NOCHECK ADD  CONSTRAINT [FK_Page_Users] FOREIGN KEY([user_id_originator])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageUserViewers_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageUserViewers]'))
ALTER TABLE [dbo].[PageUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageUserViewers_Page] FOREIGN KEY([pageID])
REFERENCES [dbo].[Page] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageUserViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageUserViewers]'))
ALTER TABLE [dbo].[PageUserViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageUserViewers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageUserEditors_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageUserEditors]'))
ALTER TABLE [dbo].[PageUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageUserEditors_Page] FOREIGN KEY([pageID])
REFERENCES [dbo].[Page] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageUserEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageUserEditors]'))
ALTER TABLE [dbo].[PageUserEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageUserEditors_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StrategyTable_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[StrategyTable]'))
ALTER TABLE [dbo].[StrategyTable]  WITH CHECK ADD  CONSTRAINT [FK_StrategyTable_Users] FOREIGN KEY([owner_id])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users] FOREIGN KEY([user_id_to])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invitation_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invitation]'))
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users1] FOREIGN KEY([user_id_from])
REFERENCES [dbo].[Users] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageGroupViewers_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageGroupViewers]'))
ALTER TABLE [dbo].[PageGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageGroupViewers_Page] FOREIGN KEY([pageID])
REFERENCES [dbo].[Page] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageGroupViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageGroupViewers]'))
ALTER TABLE [dbo].[PageGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageGroupViewers_Users] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageGroupEditors_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageGroupEditors]'))
ALTER TABLE [dbo].[PageGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageGroupEditors_Page] FOREIGN KEY([pageID])
REFERENCES [dbo].[Page] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageGroupEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageGroupEditors]'))
ALTER TABLE [dbo].[PageGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageGroupEditors_Users] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementMap_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementMap]'))
ALTER TABLE [dbo].[PageElementMap]  WITH NOCHECK ADD  CONSTRAINT [FK_PageElementMap_PageElement] FOREIGN KEY([pageElement_id])
REFERENCES [dbo].[PageElement] ([id])
GO
ALTER TABLE [dbo].[PageElementMap] CHECK CONSTRAINT [FK_PageElementMap_PageElement]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementGroupEditors_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementGroupEditors]'))
ALTER TABLE [dbo].[PageElementGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageElementGroupEditors_PageElement] FOREIGN KEY([peID])
REFERENCES [dbo].[PageElement] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementGroupEditors_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementGroupEditors]'))
ALTER TABLE [dbo].[PageElementGroupEditors]  WITH CHECK ADD  CONSTRAINT [FK_PageElementGroupEditors_Users] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementGroupViewers_PageElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementGroupViewers]'))
ALTER TABLE [dbo].[PageElementGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageElementGroupViewers_PageElement] FOREIGN KEY([peID])
REFERENCES [dbo].[PageElement] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageElementGroupViewers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageElementGroupViewers]'))
ALTER TABLE [dbo].[PageElementGroupViewers]  WITH CHECK ADD  CONSTRAINT [FK_PageElementGroupViewers_Users] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_emailMessage_emailMessageStates]') AND parent_object_id = OBJECT_ID(N'[dbo].[emailMessage]'))
ALTER TABLE [dbo].[emailMessage]  WITH CHECK ADD  CONSTRAINT [FK_emailMessage_emailMessageStates] FOREIGN KEY([state])
REFERENCES [dbo].[emailMessageStates] ([id])
