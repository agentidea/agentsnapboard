USE [AgentStory]
GO
/****** Object:  View [dbo].[vStoryPageLibrary]    Script Date: 08/23/2007 17:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vStoryPageLibrary]
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

GO