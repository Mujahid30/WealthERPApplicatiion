ALTER PROCEDURE [dbo].[SP_GetWERPUploadRejectsTransaction]
@processId INT
,@currentPage INT

AS

SET NOCOUNT ON

	IF(@currentPage IS NULL)   
	BEGIN
		
		select 
			CTS.CMFXTS_Id WERPTransactionId
			,CTS.ADUL_ProcessId ProcessID
			,CUST.C_FirstName + CUST.C_LastName as WERPCustomerName
			,case when CTS.CMFXTS_IsFolioNew = 0  
				then 'Y'  
				else 'N' end AS FolioExists
			,CTS.CMFXTS_FolioNum Folio
			,CTS.CMFXTS_TransactionNumber AS TransactionNumber
			,case when CTS.CMFXTS_IsRejected = 1  
				then 'Y'  
				else 'N' end as IsRejected
			,CTS.WRR_RejectReasonCode
		FROM
			dbo.dbo.CustomerMFXtrnlTransactionStaging AS CTS  
			LEFT OUTER JOIN Customer AS CUST 
			ON CUST.C_CustomerId = CTS.C_CustomerId  
		WHERE
		CTS.ADUL_ProcessId = @processId  
		
	END
	
	ELSE IF(@currentPage IS NOT NULL)
	BEGIN
		
		DECLARE @intStartRow int;   
		DECLARE @intEndRow int;  
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;    
		SET @intEndRow = @CurrentPage * 10;
		
		WITH Entries AS  
		(  
			 SELECT
				ROW_NUMBER() over ( ORDER BY CTS.ADUL_ProcessId ASC ) as RowNum 
				,CTS.CMFXTS_Id WERPTransactionId
				,CTS.ADUL_ProcessId ProcessID
				,CUST.C_FirstName + CUST.C_LastName as WERPCustomerName
				,case when CTS.CMFXTS_IsFolioNew = 0  
					then 'Y'  
					else 'N' end AS FolioExists
				,CTS.CMFXTS_FolioNum Folio
				,CTS.CMFXTS_TransactionNumber AS TransactionNumber
				,case when CTS.CMFXTS_IsRejected = 1  
					then 'Y'  
					else 'N' end as IsRejected
				,CTS.WRR_RejectReasonCode  
			FROM
				dbo.dbo.CustomerMFXtrnlTransactionStaging AS CTS  
				LEFT OUTER JOIN Customer AS CUST 
				ON CUST.C_CustomerId = CTS.C_CustomerId  
			WHERE
				CTS.ADUL_ProcessId = @processId    
		)  
		
		SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT 
		FROM
			dbo.dbo.CustomerMFXtrnlTransactionStaging AS CTS  
			LEFT OUTER JOIN Customer AS CUST 
			ON CUST.C_CustomerId = CTS.C_CustomerId  
		WHERE
			CTS.ADUL_ProcessId = @processId 
		
	END 

SET NOCOUNT OFF
 