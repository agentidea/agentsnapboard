use AgentStory
GO
DELETE from Groups
go
SET IDENTITY_INSERT Groups ON
GO
INSERT INTO Groups
           (id,
			[name]
           ,[dateAdded]
           ,[groupStartedBy]
           ,[guid]
           ,[description])
     VALUES
           (1
			,'system'
           ,getDate()
           ,1
           ,newID()
           ,'the system group')
GO

SET IDENTITY_INSERT Groups OFF
GO
select * from groups