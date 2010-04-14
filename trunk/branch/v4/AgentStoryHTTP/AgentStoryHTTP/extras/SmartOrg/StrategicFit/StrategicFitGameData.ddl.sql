IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StrategicFitGameData]') AND type in (N'U'))
DROP TABLE [dbo].[StrategicFitGameData]
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StrategicFitGameData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[alias] [nvarchar](100) NOT NULL,
	[tx_id] [nvarchar](100) NOT NULL,
	[lastEditedDay] [int] NOT NULL,
	[lastEditedMonth] [int] NOT NULL,
	[lastEditedYear] [int] NOT NULL,
	[lastEditedWhen] [datetime] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	
	 CameraLink 	int				NULL,
	 ClothPrinter 	int			NULL,
	 CreativeStudio	int		 NULL,
	 Cutter 	int			NULL,
	 FullPage 	int			NULL,
	 PhotoKiosk 	int			NULL,
	 PreciseDose 	int		NULL,
	 RealPhoto 	int		NULL,
	 RPTV 		int	NULL,
	 SpotInks 	int		NULL,


	
 CONSTRAINT [PK_StrategicFitGameData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[StrategicFitGameData] ADD  DEFAULT (newid()) FOR [guid]
GO

ALTER TABLE dbo.StrategicFitGameData ADD CONSTRAINT
	IX_StrategicFitGameData_TX_ID UNIQUE NONCLUSTERED 
	(
	tx_id
	) WITH(  IGNORE_DUP_KEY = OFF )

GO
