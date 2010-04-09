/*delete from StoryTuple where StoryID = 3
go

delete from Tuple
go
*/

DECLARE @RC int
DECLARE @name nvarchar(100)
DECLARE @code nvarchar(100)
DECLARE @description nvarchar(254)
DECLARE @guid uniqueidentifier
DECLARE @units nvarchar(100)
DECLARE @numValue decimal(18,4)
DECLARE @pk int

set @name = 'Attila the Hun died'
set @code = 'AttilaHunDied'
set @description = 'VGhlIHllYXIgaW4gd2hpY2ggQXR0aWxhIHRoZSBIdW4gZGllZA=='
set @guid = NEWID()
set @units = 'positive for A.D. negative for B.C.'
set @numValue = 453

EXECUTE @RC = [AgentStoryEvolution].[dbo].[addTuple] 
   @name
  ,@code
  ,@description
  ,@guid
  ,@units
  ,null
  ,@numValue
  ,@pk OUTPUT
  
  
  
  insert into StoryTuple (StoryID,TupleID) values ( 3,@pk)

GO

DECLARE @RC int
DECLARE @name nvarchar(100)
DECLARE @code nvarchar(100)
DECLARE @description nvarchar(254)
DECLARE @guid uniqueidentifier
DECLARE @units nvarchar(100)
DECLARE @numValue decimal(18,4)
DECLARE @pk int

set @name = 'US Auto Thefts'
set @code = 'USAutoThefts'
set @description = 'TnVtYmVyIG9mIGF1dG8gdGhlZnRzIGluIHRoZSBVLlMuIGluIDE5OTY='
set @guid = NEWID()
set @units = 'thousands'
set @numValue = 1394

EXECUTE @RC = [AgentStoryEvolution].[dbo].[addTuple] 
   @name
  ,@code
  ,@description
  ,@guid
  ,@units
  ,null
  ,@numValue
  ,@pk OUTPUT
  
  
  
  insert into StoryTuple (StoryID,TupleID) values ( 3,@pk)

GO

DECLARE @RC int
DECLARE @name nvarchar(100)
DECLARE @code nvarchar(100)
DECLARE @description nvarchar(254)
DECLARE @guid uniqueidentifier
DECLARE @units nvarchar(100)
DECLARE @numValue decimal(18,4)
DECLARE @pk int

set @name = 'US Beef Consumption'
set @code = 'USBeefConsumption'
set @description = 'VS5TLiBjb25zdW1wdGlvbiBvZiBiZWVmIGluIDE5OTc='
set @guid = NEWID()
set @units = 'millions of pounds'
set @numValue = 25609

EXECUTE @RC = [AgentStoryEvolution].[dbo].[addTuple] 
   @name
  ,@code
  ,@description
  ,@guid
  ,@units
  ,null
  ,@numValue
  ,@pk OUTPUT
  
  
  
  insert into StoryTuple (StoryID,TupleID) values ( 3,@pk)

GO

DECLARE @RC int
DECLARE @name nvarchar(100)
DECLARE @code nvarchar(100)
DECLARE @description nvarchar(254)
DECLARE @guid uniqueidentifier
DECLARE @units nvarchar(100)
DECLARE @numValue decimal(18,4)
DECLARE @pk int

set @name = 'Pluto Orbit Duration'
set @code = 'PlutoOrbit'
set @description = 'TnVtYmVyIG9mIHllYXJzIGl0IHRha2VzIFBsdXRvIHRvIGNpcmN1bW5hdmlnYXRlIHRoZSBzdW4='
set @guid = NEWID()
set @units = 'Earth years'
set @numValue = 247.7

EXECUTE @RC = [AgentStoryEvolution].[dbo].[addTuple] 
   @name
  ,@code
  ,@description
  ,@guid
  ,@units
  ,null
  ,@numValue
  ,@pk OUTPUT
  
  
  
  insert into StoryTuple (StoryID,TupleID) values ( 3,@pk)

GO

DECLARE @RC int
DECLARE @name nvarchar(100)
DECLARE @code nvarchar(100)
DECLARE @description nvarchar(254)
DECLARE @guid uniqueidentifier
DECLARE @units nvarchar(100)
DECLARE @numValue decimal(18,4)
DECLARE @pk int

set @name = 'Beijing to Moscow'
set @code = 'BeijingToMoscow'
set @description = 'QWlybGluZSBkaXN0YW5jZSBmcm9tIEJlaWppbmcsIENoaW5hIHRvIE1vc2NvdywgUnVzc2lh'
set @guid = NEWID()
set @units = 'miles'
set @numValue = 3607

EXECUTE @RC = [AgentStoryEvolution].[dbo].[addTuple] 
   @name
  ,@code
  ,@description
  ,@guid
  ,@units
  ,null
  ,@numValue
  ,@pk OUTPUT
  
  
  
  insert into StoryTuple (StoryID,TupleID) values ( 3,@pk)

GO
