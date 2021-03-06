USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspSaveMinCustomer]    Script Date: 12/22/2017 18:59:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sspSaveMinCustomer] @TenantID int, @CustName varchar (255), @CustAddress1 varchar (50),
		@CustCity  varchar(50), @State varchar(50),@Country varchar(50), @custPhone varchar(30), @Email varchar(255), @CustID int = 0, @SpecimenID int = 0,@Zipcode varchar(30),
		@Facility varchar(50),@Fax varchar(30),@Specialization nvarchar(50)
		--@Diagnosis varchar(50),@DiagnosisCode varchar(50),
 --@ResultType varchar(10) ='',
AS
begin
Declare @CustNum varchar(30) = ''

	if (@CustID = 0)
	Begin

		exec dbo.sspGetNextSeqNumber @TenantID,'Customer', 0,@CustNum output
		
			Insert stblCustomer(CustNumber,CustomerName,Address1, City,[State], Country, Email, Phone, MemberSince,CustStatus,Facility,Fax,Zipcode,TenantID,Specialization)
				Values(@CustNum,@custName, @custAddress1, @custCity,@State,@Country, @Email, @custPhone, getdate(),'Active',@Facility,@Fax,@Zipcode,@TenantID,@Specialization)

			set @CustID = @@identity 

			If @SpecimenID <> 0
				Update stblSpecimenInfo set CustomerID = @CustID where SpecimenID = @SpecimenID
	end
	else
	Begin
		select @CustNum = CustNumber from stblCustomer where CustID=@CustID

		Update stblCustomer set CustomerName = @custName, Phone = @custPhone, Address1 = @custAddress1, 
			City = @custCity, [State] = @State, Email = @Email,Facility=@Facility,Fax = @Fax,Zipcode = @Zipcode,TenantID=@TenantID,Specialization=@Specialization where custID = @CustID

			If @SpecimenID <> 0
				Update stblSpecimenInfo set CustomerID = @CustID where SpecimenID = @SpecimenID
	End

	select @CustID as CustomerID, @CustNum as CustomerNumber

end