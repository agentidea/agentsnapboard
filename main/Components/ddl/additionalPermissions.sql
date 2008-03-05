BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT





CREATE TABLE dbo.PageElementUserViewers
	(
	peID int NOT NULL,
	user_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageElementUserViewers ADD CONSTRAINT
	PK_PageElementUserViewers PRIMARY KEY CLUSTERED 
	(
	peID,
	user_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageElementUserViewers ADD CONSTRAINT
	FK_PageElementUserViewers_Users FOREIGN KEY
	(
	user_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


CREATE TABLE dbo.PageElementUserEditors
	(
	peID int NOT NULL,
	user_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageElementUserEditors ADD CONSTRAINT
	PK_PageElementUserEditors PRIMARY KEY CLUSTERED 
	(
	peID,
	user_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageElementUserEditors ADD CONSTRAINT
	FK_PageElementUserEditors_Users FOREIGN KEY
	(
	user_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

CREATE TABLE dbo.PageElementGroupViewers
	(
	peID int NOT NULL,
	group_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageElementGroupViewers ADD CONSTRAINT
	PK_PageElementGroupViewers PRIMARY KEY CLUSTERED 
	(
	peID,
	group_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageElementGroupViewers ADD CONSTRAINT
	FK_PageElementGroupViewers_Users FOREIGN KEY
	(
	group_id
	) REFERENCES dbo.Groups
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


CREATE TABLE dbo.PageElementGroupEditors
	(
	peID int NOT NULL,
	group_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageElementGroupEditors ADD CONSTRAINT
	PK_PageElementGroupEditors PRIMARY KEY CLUSTERED 
	(
	peID,
	group_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageElementGroupEditors ADD CONSTRAINT
	FK_PageElementGroupEditors_Users FOREIGN KEY
	(
	group_id
	) REFERENCES dbo.Groups
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO



CREATE TABLE dbo.PageUserViewers
	(
	pageID int NOT NULL,
	user_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageUserViewers ADD CONSTRAINT
	PK_PageUserViewers PRIMARY KEY CLUSTERED 
	(
	pageID,
	user_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageUserViewers ADD CONSTRAINT
	FK_PageUserViewers_Users FOREIGN KEY
	(
	user_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


CREATE TABLE dbo.PageUserEditors
	(
	pageID int NOT NULL,
	user_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageUserEditors ADD CONSTRAINT
	PK_PageUserEditors PRIMARY KEY CLUSTERED 
	(
	pageID,
	user_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageUserEditors ADD CONSTRAINT
	FK_PageUserEditors_Users FOREIGN KEY
	(
	user_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

CREATE TABLE dbo.PageGroupViewers
	(
	pageID int NOT NULL,
	group_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageGroupViewers ADD CONSTRAINT
	PK_PageGroupViewers PRIMARY KEY CLUSTERED 
	(
	pageID,
	group_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageGroupViewers ADD CONSTRAINT
	FK_PageGroupViewers_Users FOREIGN KEY
	(
	group_id
	) REFERENCES dbo.Groups
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


CREATE TABLE dbo.PageGroupEditors
	(
	pageID int NOT NULL,
	group_id int NOT NULL,
	dateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PageGroupEditors ADD CONSTRAINT
	PK_PageGroupEditors PRIMARY KEY CLUSTERED 
	(
	pageID,
	group_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PageGroupEditors ADD CONSTRAINT
	FK_PageGroupEditors_Users FOREIGN KEY
	(
	group_id
	) REFERENCES dbo.Groups
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO






ALTER TABLE dbo.PageGroupViewers ADD CONSTRAINT
	FK_PageGroupViewers_Page FOREIGN KEY
	(
	pageID
	) REFERENCES dbo.Page
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


ALTER TABLE dbo.PageUserViewers ADD CONSTRAINT
	FK_PageUserViewers_Page FOREIGN KEY
	(
	pageID
	) REFERENCES dbo.Page
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageGroupEditors ADD CONSTRAINT
	FK_PageGroupEditors_Page FOREIGN KEY
	(
	pageID
	) REFERENCES dbo.Page
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageUserEditors ADD CONSTRAINT
	FK_PageUserEditors_Page FOREIGN KEY
	(
	pageID
	) REFERENCES dbo.Page
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageElementGroupViewers ADD CONSTRAINT
	FK_PageElementGroupViewers_PageElement FOREIGN KEY
	(
	peID
	) REFERENCES dbo.PageElement
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageElementGroupEditors ADD CONSTRAINT
	FK_PageElementGroupEditors_PageElement FOREIGN KEY
	(
	peID
	) REFERENCES dbo.PageElement
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageElementUserViewers ADD CONSTRAINT
	FK_PageElementUserViewers_PageElement FOREIGN KEY
	(
	peID
	) REFERENCES dbo.PageElement
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.PageElementUserEditors ADD CONSTRAINT
	FK_PageElementUserEditors_PageElement FOREIGN KEY
	(
	peID
	) REFERENCES dbo.PageElement
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO



