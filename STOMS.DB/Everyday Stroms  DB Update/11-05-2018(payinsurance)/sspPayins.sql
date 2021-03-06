USE [kosoft_Stg_Storms]
GO
/****** Object:  StoredProcedure [dbo].[sspSavePayment]    Script Date: 05-11-2018 17:46:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sspSavePayment]

@PaymentID int,@PaymentMode varchar(50) = ' ',@PaymentStatus varchar(50) = ' ', @TenantID int = 0,@SpecimenID int = 0,@Cash decimal(18, 0) = 0, @TransactionDate varchar(30) = ' ',
@Currency varchar(30) = ' ',@Description text = ' ',@CardType varchar(50) = ' ',@CardNumber varchar(150) = ' ',@HolderName varchar(50) = ' ',@CVVNumber varchar(50) =' ',
@ExpireDate varchar(50) = ' ',@CreditAmount decimal(18, 0) = 0,@BankName varchar(50) = ' ',@BranchName varchar(50) = ' ',@ChequeNumber varchar(20) = ' ',
@ChequeDate varchar(50) = ' ',@ChequeAmount decimal(18, 0) = 0,@AccountNumber varchar(30) = ' ',@RoutingNumber varchar(30) = ' ',
@MemoDescription text = ' ',@ChequeUpload bit = 'false',@InsuranceType varchar(50) = ' ',@InsuranceCompany varchar(50) = ' ',
@InsuranceNumber varchar(50) = ' ',@MemberName varchar(50) = ' ',@MemberShipNumber varchar(50) = ' ',
@GroupNumber varchar(30) = ' ',@PreAuthCode varchar(30) = ' ',@PreInsuranceNo int= 0

as
begin

if(@PaymentID = 0)
begin
insert into stblPayment(TenantID,SpecimenID,PaymentMode, PaymentStatus, Cash, TransactionDate, Currency, Description, CardType,CardNumber, HolderName, CVVNumber, ExpireDate,
			CreditAmount,BankName, BranchName, ChequeNumber, ChequeDate, ChequeAmount, AccountNumber, RoutingNumber, MemoDescription, ChequeUpload,
			InsuranceType, InsuranceCompany, InsuranceNumber,MemberName, MemberShipNumber, GroupNumber,PreAuthCode,PreInsuranceNo)

            values(@TenantID,@SpecimenID,@PaymentMode,@PaymentStatus,@Cash, @TransactionDate, @Currency, @Description, @CardType, @CardNumber, @HolderName, @CVVNumber,
			 @ExpireDate,@CreditAmount,@BankName, @BranchName, @ChequeNumber, @ChequeDate, @ChequeAmount,@AccountNumber,@RoutingNumber, 
			 @MemoDescription,@ChequeUpload, @InsuranceType, @InsuranceCompany, @InsuranceNumber, @MemberName, @MemberShipNumber, 
			 @GroupNumber,@PreAuthCode,@PreInsuranceNo)

			set @PaymentID = @@IDENTITY
			select @PaymentID as PaymentID
end
else

update stblPayment set PaymentMode = @PaymentMode, PaymentStatus = @PaymentStatus, Cash = @Cash, TransactionDate = @TransactionDate, Currency = @Currency,
					 Description = @Description, CardType = @CardType, CardNumber = @CardNumber, HolderName = @HolderName, CVVNumber = @CVVNumber,
					  ExpireDate = @ExpireDate,CreditAmount = @CreditAmount,BankName = @BankName, BranchName = @BranchName, ChequeNumber = @ChequeNumber,
					   ChequeDate = ChequeDate, ChequeAmount = @ChequeAmount, AccountNumber = @AccountNumber, RoutingNumber = @RoutingNumber, 
					   MemoDescription = @MemoDescription, ChequeUpload = @ChequeUpload,InsuranceType = @InsuranceType, InsuranceCompany = @InsuranceCompany,
					    InsuranceNumber = @InsuranceNumber, MemberName = @MemberName,MemberShipNumber = @MemberShipNumber, GroupNumber = @GroupNumber, 
						PreAuthCode = @PreAuthCode,PreInsuranceNo=@PreInsuranceNo  where PaymentID = @PaymentID and TenantID = @TenantID
end

