


ALTER PROCEDURE [dbo].[SP_GetUploadRejects]
	-- Add the parameters for the stored procedure here
	@processId int,
	@adviserId int,
	@typeId varchar(25)
	
AS
	SET NOCOUNT ON
	
	if @typeId = 'CAMSProfile'
	begin
	
	select CPS.ADUL_ProcessId ProcessID, CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName, 
	case when CPS.CMGCXPS_IsCustomerNew = 0
	then 'Y'
	else 'N' end as CustomerExists,
	CPS.CMGCXPS_INV_NAME NAME, CPS.CMGCXPS_PAN_NO PANNumber, CPS.CMGCXPS_FOLIOCHK Folio, 
	case when CPS.CMGCXPS_IsRejected = 1
	then 'Y'
	else 'N' end as IsRejected,
	CPS.CMGCXPS_ADDRESS1 + CPS.CMGCXPS_ADDRESS2 + CPS.CMGCXPS_ADDRESS3 as CUSTADDRESS,
	CPS.CMGCXPS_PINCODE
	from CustomerMFCAMSXtrnlProfileStaging CPS
	left outer join Customer CUST on
	CUST.C_CustomerId = CPS.C_CustomerId
	where CPS.ADUL_ProcessId = @processId
	and CPS.A_AdviserId = @adviserId
	end
	
	else if @typeId = 'CAMSTransaction'
	begin
	
	select CTS.ADUL_ProcessId ProcessID, CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName,
	case when CTS.CMCXTS_IsFolioNew = 0
	then 'Y'
	else 'N' end as FolioExists,
	CTS.CMCXTS_TransactionNum TransactionNumber,
	case when CTS.CMCXTS_IsRejected = 1
	then 'Y'
	else 'N' end as IsRejected
	from CustomerMFCAMSXtrnlTransactionStaging CTS
	left outer join Customer CUST on
	CUST.C_CustomerId = CTS.C_CustomerId
	where CTS.ADUL_ProcessId = @processId
	and CTS.A_AdviserId = @adviserId
	
	end
	
	else if @typeId = 'KarvyProfile'
	begin
	
	select KPS.ADUL_ProcessId ProcessID, CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName, 
	case when KPS.CMFKXPS_IsCustomerNew = 0
	then 'Y'
	else 'N' end as CustomerExists,
	KPS.CMFKXPS_InvestorName Name, KPS.CMFKXPS_PANNumber PanNumber, KPS.CMFKXPS_Folio Folio,
	case when KPS.CMFKXPS_IsRejected = 1
	then 'Y'
	else 'N' end as IsRejected,
	KPS.CMFKXPS_Address#1 + KPS.CMFKXPS_Address#2 + KPS.CMFKXPS_Address#2 as CUSTADDRESS,
	KPS.CMFKXPS_Pincode
	 from CustomerMFKarvyXtrnlProfileStaging KPS
	 left outer join Customer CUST on
	CUST.C_CustomerId = KPS.C_CustomerId
	where KPS.ADUL_ProcessId = @processId
	and KPS.A_AdviserId = @adviserId
	
	end
	else if @typeId = 'KarvyTransaction'
	begin
	select KTS.ADUL_ProcessId ProcessID, CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName, 
	case when KTS.CIMFKXTS_IsFolioNew = 0
	then 'Y'
	else 'N' end as FolioExists,
	KTS.CIMFKXTS_TransactionNumber TransactionNumber,
	case when KTS.CIMFKXTS_IsRejected = 1
	then 'Y'
	else 'N' end as IsRejected
	from dbo.CustomerMFKarvyXtrnlTransactionStaging KTS
	left outer join Customer CUST on
	CUST.C_CustomerId = KTS.C_CustomerId
	where KTS.ADUL_ProcessId = @processId
	and KTS.A_AdviserId = @adviserId
	end
	
	SET NOCOUNT OFF


 