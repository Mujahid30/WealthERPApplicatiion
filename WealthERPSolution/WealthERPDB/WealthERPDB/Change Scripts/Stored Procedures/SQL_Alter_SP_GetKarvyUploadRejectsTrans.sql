  
ALTER PROCEDURE [dbo].[SP_GetKarvyUploadRejectsTrans]  
 -- Add the parameters for the stored procedure here  
 @processId INT,
 @currentPage INT  
   
AS  
 SET NOCOUNT ON  
 
	IF(@CurrentPage IS NULL)   
	BEGIN
		
		SELECT 
			KTS.CIMFKXTS_Id AS KarvyTranStagingID
			,KTS.ADUL_ProcessId AS ProcessID
			,CUST.C_FirstName + CUST.C_LastName AS WERPCustomerName
			,case when KTS.CIMFKXTS_IsFolioNew = 0  
				then 'Y'  
				else 'N' end as FolioExists
			,KTS.CIMFKXTS_SchemeCode AS SchemeCode
			,KTS.CIMFKXTS_TransactionNumber AS TransactionNumber
			,case when KTS.CIMFKXTS_IsRejected = 1  
				then 'Y'  
				else 'N' end as IsRejected
			,KTS.WRR_RejectReasonCode AS RejectCode
		FROM 
			dbo.CustomerMFKarvyXtrnlTransactionStaging AS KTS  
			LEFT OUTER JOIN Customer AS CUST
			ON CUST.C_CustomerId = KTS.C_CustomerId  
		WHERE
			KTS.ADUL_ProcessId = @processId
		
	END
	ELSE IF (@CurrentPage IS NOT NULL)
	BEGIN
		
		DECLARE @intStartRow int;   
		DECLARE @intEndRow int;  
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;    
		SET @intEndRow = @CurrentPage * 10;
		
		WITH Entries AS  
		(  
			 SELECT
				ROW_NUMBER() over ( ORDER BY KTS.ADUL_ProcessId ASC ) as RowNum 
				,KTS.CIMFKXTS_Id AS KarvyTranStagingID
				,KTS.ADUL_ProcessId AS ProcessID
				,CUST.C_FirstName + CUST.C_LastName AS WERPCustomerName
				,case when KTS.CIMFKXTS_IsFolioNew = 0  
					then 'Y'  
					else 'N' end as FolioExists
				,KTS.CIMFKXTS_SchemeCode AS SchemeCode
				,KTS.CIMFKXTS_TransactionNumber AS TransactionNumber
				,case when KTS.CIMFKXTS_IsRejected = 1  
					then 'Y'  
					else 'N' end as IsRejected
				,KTS.WRR_RejectReasonCode AS RejectCode
			FROM 
				dbo.CustomerMFKarvyXtrnlTransactionStaging AS KTS  
				LEFT OUTER JOIN Customer AS CUST
				ON CUST.C_CustomerId = KTS.C_CustomerId  
			WHERE
				KTS.ADUL_ProcessId = @processId   
		)  
		
		SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT 
		FROM
			dbo.CustomerMFKarvyXtrnlTransactionStaging KTS  
			left outer join Customer CUST on  
			CUST.C_CustomerId = KTS.C_CustomerId  
		WHERE
			KTS.ADUL_ProcessId = @processId
		
	END
   
 SET NOCOUNT OFF
 