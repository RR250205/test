USE [kosoft_Stg_Storms]
GO
/****** Object:  StoredProcedure [dbo].[sspSaveInsurance]    Script Date: 04-25-2018 12:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspSaveInsurance]
  @TenantID int, 
  @PatientName nvarchar(50),
  @Gender nvarchar(50),
  @Dataofbirth nvarchar(50),
  @InsuranceCard_IDno nvarchar(50),
  @MobileNumber nvarchar(50),
  @PolicyName nvarchar(50),
  @PolicyNumber nvarchar(50),
  @PrimaryInsName nvarchar(50),
  @PreInsuranceNo int =0,
  @Comments nvarchar(50)=''

as
begin
	
	 --Declare @PreInsuranceNo as int
	 if(@PreInsuranceNo=0)

	 begin
	 INSERT INTO stblInsurancePreauthorization(TenantID ,PatientName ,Gender,Dataofbirth,InsuranceCard_IDno, MobileNumber, PolicyName, PolicyNumber,PrimaryInsName,Createon,Status)
		VALUES(@TenantID ,@PatientName ,@Gender,@Dataofbirth,@InsuranceCard_IDno, @MobileNumber, @PolicyName, @PolicyNumber,@PrimaryInsName,getdate(),'Received')
	 
	 select @@identity as PreInsuranceNo
	 end 
	 
	 else
	  begin

	  update stblInsurancePreauthorization set TenantID=@TenantID,PatientName=@PatientName,Gender=@Gender,Dataofbirth=@Dataofbirth,InsuranceCard_IDno=@InsuranceCard_IDno,MobileNumber=@MobileNumber,PolicyName=@PolicyName, PolicyNumber=@PolicyNumber,PrimaryInsName=@PrimaryInsName,Createon=getdate(),Status='Submitted',Comments=@Comments where PreInsuranceNo=@PreInsuranceNo 

	  select @@identity as PreInsuranceNo
	  end
	
		



	
	
End


GO
