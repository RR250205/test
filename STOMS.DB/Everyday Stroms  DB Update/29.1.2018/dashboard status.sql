USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspGetTenantDashboardStats]    Script Date: 2018-01-28 11:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sspGetTenantDashboardStats] @OrgID int, @ActivityMonth int, @ActivityYear int
AS
BEGIN

Declare @TotalOrders int
Declare @SampleTesting int
Declare @ReceivedStat int
Declare @ReadyStat int
Declare @AssignStat int
Declare @ClientCount int
Declare @OutStandingInv int

	select ClientID into #tbl1 from stblClient where OrgID = @OrgID

	--select orderID,OrderDate into #tbl2 from stblOrder where CustID in 
	--	(select CustID from stblCustomer where TenantID in 
	--		(select ClientID from #tbl1))
	select orderID,OrderDate into #tbl2 from stblOrder where TenantID = @OrgID

	select @TotalOrders = count(*) from #tbl2 where month(OrderDate) = @ActivityMonth and year(OrderDate) = @ActivityYear

	--select @SampleTesting = count(*) from stblSampleTestTrack where PatientID in 
	--	(select PatientID from stblPatient where OrderID in 
	--		(Select OrderID from #tbl2))

	select @SampleTesting = count(*) from stblSpecimenInfo where 
		SpecimenStatus in ('Received','Ready for Assay','Assigned to Assay') and TenantID = @OrgID -- in (select ClientID from #tbl1)

		select @ReceivedStat = count(*) from stblSpecimenInfo where 
		SpecimenStatus in ('Received') and TenantID = @OrgID

		select @ReadyStat = count(*) from stblSpecimenInfo where 
		SpecimenStatus in ('Ready for Assay') and TenantID = @OrgID

		select @AssignStat = count(*) from stblSpecimenInfo where 
		SpecimenStatus in ('Assigned to Assay') and TenantID = @OrgID

	--select @ClientCount = count(*) from #tbl1
	select @ClientCount = count(*) from stblCustomer where TenantID= @OrgID

	select @OutStandingInv = count(*) from stblInvoice where OrderID in (select OrderID from #tbl2)
		and invStatus = 'Open'

	Select @TotalOrders as TotalOrders, @SampleTesting as TestInProgress,@ReceivedStat as ReceivedSpecimens,
		   @ReadyStat as ReadyforAssaySpecimens, @AssignStat as AssigntoAssaySpecimens, @ClientCount as ClientCount,
		   @OutStandingInv as OutStandingInv
END