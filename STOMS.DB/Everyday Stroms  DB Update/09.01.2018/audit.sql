ALTER TABLE stblActionAuditLog
ALTER COLUMN ActionName nvarchar(50);

USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspSaveActionAuditLog]    Script Date: 01/10/2018 21:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sspSaveActionAuditLog] 
	-- Add the parameters for the stored procedure here
	@EntityType varchar(30),
	@EntityID int,
	@ActionName varchar(50),
	@ActionBy varchar(30)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    insert into stblActionAuditLog(EntityType,EntityID,ActionName,ActionBy,ActionOn)values(@EntityType,@EntityID,@ActionName,@ActionBy,getdate())   
END
GO
