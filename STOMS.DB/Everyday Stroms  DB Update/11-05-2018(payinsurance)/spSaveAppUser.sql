USE [kosoft_Stg_Storms]
GO
/****** Object:  StoredProcedure [dbo].[spSaveAppUser]    Script Date: 05-11-2018 18:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSaveAppUser]
 @firstname varchar(50),
  @lastname varchar(50),
   @appusername varchar(50), 
   @password varchar(50),
    @contactNo varchar(30),
	@status varchar(20),
	@kproductID int,
	@createdby int,
	@tenantID int,
	@appuserId  int,
	@UserType varchar(30),
	@defaultPage varchar(50) = '~/pages/dashboard'
	
AS
BEGIN

declare  @tmpUserID int =0, @subscriptionID int
if @appuserId =0 
	begin
	select @tmpUserID=AppUserID from tblEntAppUser where AppUserName=@appusername;
		if @tmpUserID=0
		begin
			Insert into tblEntAppUser (FirstName, LastName,AppUserName, AppUserPassword, ContactPhone, AppUserStatus,PrimaryCompanyID,PrimaryEmail,UserType) 
				values(@firstname,@lastname, @appusername,@password,@contactNo, @status,@tenantID,@appusername,@UserType)
				set @appuserId=@@IDENTITY;

			select @subscriptionID =SubscriptionID from tblEntSubscribedProd where TenantID=@tenantID and KProductID=@kProductID
			insert into tblEntAppUserProduct (AppUserID, SubscriptionID, AssociatedOn, AssoStatus, DefaultPage) 
			values(@appuserId, @subscriptionID, getdate(),'Active',@defaultPage)
		end
	end
	select @appuserId
END


GO
