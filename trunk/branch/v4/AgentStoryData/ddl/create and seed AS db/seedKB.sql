
--seed the agentstoryevolution db

--add db user

use [AgentStoryEvolution]
GO
CREATE USER agentstorydbo FOR LOGIN agentstorydbo
GO 

EXEC sp_addrolemember N'db_owner', N'agentstorydbo'
GO


DELETE FROM [AgentStoryEvolution].[dbo].[StoryState]
GO

INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (0,'inactive')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (1,'active')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (2,'archived')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (3,'deactivated')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (4,'suspect')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[StoryState]([id],[stateName])VALUES (5,'deleted')
GO


DELETE FROM [AgentStoryEvolution].[dbo].[PageElementType]
GO

INSERT INTO [AgentStoryEvolution].[dbo].[PageElementType]([id],[code],[name])VALUES (1,'TXT','text')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[PageElementType]([id],[code],[name])VALUES (2,'AUD','audio')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[PageElementType]([id],[code],[name])VALUES (3,'VID','video')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[PageElementType]([id],[code],[name])VALUES (4,'IMG','image')
GO
INSERT INTO [AgentStoryEvolution].[dbo].[PageElementType]([id],[code],[name])VALUES (5,'RND','random')
GO

DELETE from UsersGroups
GO
DELETE FROM [USERS]
GO
DELETE FROM Groups
GO



SET IDENTITY_INSERT Groups ON
GO
--system
INSERT INTO Groups
           (id,
			[name]
           ,[dateAdded]
           ,[groupStartedBy]
           ,[guid]
           ,[description])
     VALUES
           (1
			,'c3lzdGVt'
           ,getDate()
           ,1
           ,newID()
           ,'U3lzdGVtVXNlcnM=')
GO



--public
INSERT INTO Groups
           (id,
			[name]
           ,[dateAdded]
           ,[groupStartedBy]
           ,[guid]
           ,[description])
     VALUES
           (2
			,'cHVibGlj'
           ,getDate()
           ,1
           ,newID()
           ,'cHVibGlj')
GO

--everyone
INSERT INTO Groups
           (id,
			[name]
           ,[dateAdded]
           ,[groupStartedBy]
           ,[guid]
           ,[description])
     VALUES
           (3
			,'ZXZlcnlvbmU='
           ,getDate()
           ,1
           ,newID()
           ,'dGhlIGdyb3VwIHRoYXQgZXZlcnlvbmUgYmVsb25ncyB0bw==')
GO


SET IDENTITY_INSERT Groups OFF
GO
select * from groups

GO

SET IDENTITY_INSERT [users] ON
go
  insert into [users]
  ( 
      ID,
  	  firstName,
  	  lastName,
  	  username,
  	  password,
  	  email,
  	  roles,
  	  state,
  	  origInvitationCode,
  	  dateAdded,
  	  userGUID,
	  notificationFrequency,
	  notificationTypes,tags
    )
    VALUES
    ( 3,

  	  'cHVwcHk=',
  	  'c2VuZG1haWxlcg==',
  	  'c3lzcG9zdG1hbg==',
  	  'ankxbWV0Mg==',
  	  'mail-daemon@agentidea.com',
  	  'admin',
  	  3,
  	  'x0xFFAEEAFFx0x',
  	  '11/8/2006 5:35:51 PM',
  	  '7be48322-00c5-416d-9580-0b40df936d50',
		1,'text|html','postman email'
    )
    
    GO
    
   insert into [users]
  ( 
  	  ID,
      firstName,
  	  lastName,
  	  username,
  	  password,
  	  email,
  	  roles,
  	  state,
  	  origInvitationCode,
  	  dateAdded,
  	  userGUID,
notificationFrequency,
notificationTypes,tags
    )
    VALUES
    ( 2,
  	  'bWFpbFJlbGF5',
  	  'c3lzdGVt',
  	  'c3lzbWFpbHJlbGF5',
  	  'ankxbWV0Mg==',
  	  'anon-relay@agentidea.com',
  	  'admin',
  	  3,
  	  'x0xFFAAAFFx0x',
  	  '11/8/2006 5:35:51 PM',
  	  '7be48322-00c5-416d-9580-0b40df936d50',3,'text','system'
    )
    
    GO 
  

insert into [users]
( 
	  ID,
      firstName,
	  lastName,
	  username,
	  password,
	  email,
	  roles,
	  state,
	  origInvitationCode,
	  dateAdded,
	  userGUID,
notificationFrequency,
notificationTypes,tags
  )
  VALUES
  (		5,
	  'cHVwcHk=',
	  'YWlzaGE=',
	  'YWlzaGE=',
	  'ankxbWV0Mg==',
	  'aishapup@agentidea.com ',
	  'admin',
	  3,
	  'x0xFFAE',
	  '11/8/2006 5:29:58 PM',
	  '023cdfb9-47c9-4d3b-981a-66167b4448a6',2,'html','shiba inu puppy'
  )
  
  GO
  
insert into [users]
  ( 
      ID,
  	  firstName,
  	  lastName,
  	  username,
  	  password,
  	  email,
  	  roles,
  	  state,
  	  origInvitationCode,
  	  dateAdded,
  	  userGUID,
	  notificationFrequency,
	  notificationTypes,
      tags
    )
    VALUES
    ( 1,
  	  'R3JhbnQ=',
  	  'U3RlaW5mZWxk',
  	  'R3JhbnQ=',
  	  'ankxbWV0Mg==',
  	  'g@agentidea.com',
  	  'root',
  	  3,
  	  'x0xFABBAFx0x',
  	  '10/8/2006 5:35:51 PM',
  	  '067e8dfa-4b65-4e58-8189-bbc7e142e467',
		1,'html','software engineer biologist photographer chef water scuba shiba'
    )
  go


insert into [users]
  ( 
      ID,
  	  firstName,
  	  lastName,
  	  username,
  	  password,
  	  email,
  	  roles,
  	  state,
  	  origInvitationCode,
  	  dateAdded,
  	  userGUID,
	  notificationFrequency,
	  notificationTypes,
      tags,sponsorID
    )
    VALUES
    ( 4,
  	  'am9l',
  	  'cHVibGlj',
  	  'cHVibGlj',
  	  'ankxbWV0Mg==',
  	  'joepublic@bukanator.com',
  	  'observer',
  	  3,
  	  'x0xFFABBAFFx0x',
  	  '11/8/2006 5:35:51 PM',
  	  'aac551e1-6797-4e78-8edf-ba2c9a4d4d2d',
		0,'text','public',1
    )
go




  
SET IDENTITY_INSERT [users] OFF
go

  
  SELECT * FROM [USERS]
  GO






INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			1,1,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			2,1,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			3,1,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			4,1,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			5,1,getDate()
			)

GO

INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			4,2,getDate()
			)

GO


INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			1,3,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			2,3,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			3,3,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			4,3,getDate()
			)

GO
INSERT INTO [AgentStoryEvolution].[dbo].[UsersGroups]
           ([user_id]
           ,[group_id]
           ,[dateAdded])
     VALUES
           (
			5,3,getDate()
			)

GO




select * from UsersGroups
go





DELETE FROM emailMessageStates
GO

INSERT INTO emailMessageStates
                      (id, name)
VALUES     (0, 'draft')
GO
INSERT INTO emailMessageStates
                      (id, name)
VALUES     (1, 'toBeSent')
GO
INSERT INTO emailMessageStates
                      (id, name)
VALUES     (2, 'sent')

GO

INSERT INTO emailMessageStates
                      (id, name)
VALUES     (3, 'failed')

GO

INSERT INTO emailMessageStates
                      (id, name)
VALUES     (4, 'deleted')

GO

INSERT INTO emailMessageStates
                      (id, name)
VALUES     (5, 'sending')

GO

SELECT * FROM emailMessageStates

GO