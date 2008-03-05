use AgentStory
go
DELETE FROM StoryGroupEditors
go
DELETE FROM StoryGroupViewers
go
DELETE FROM StoryUserEditors
go
DELETE FROM StoryUserViewers
go
DELETE FROM PageElementType
GO

delete from PagePageElementMap
go

delete from PageElementMap
go

delete from StoryPageElement
go
delete from PageElement
go
delete from StoryPage
go
delete from FACT_StoryUserView
go
delete from FACT_StoryView
go
delete from Page
go
delete from Story
go
delete from emailMessage
go
delete from Invitation
go



DELETE from StoryState
GO


delete from StoryUserViewers
go
delete from StoryUserEditors
go
delete from StoryGroupEditors
go
delete from StoryGroupViewers
go
delete from Invitation
go
delete from UsersGroups
go
delete from [Users]
go
delete from [Groups]
go
select * from Users