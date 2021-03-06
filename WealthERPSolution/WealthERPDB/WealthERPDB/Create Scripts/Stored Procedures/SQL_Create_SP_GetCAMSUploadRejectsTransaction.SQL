/****** Object:  StoredProcedure [dbo].[SP_GetCAMSUploadRejectsTransaction]    Script Date: 06/24/2009 11:58:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCAMSUploadRejectsTransaction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCAMSUploadRejectsTransaction]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCAMSUploadRejectsTransaction]    Script Date: 06/24/2009 11:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCAMSUploadRejectsTransaction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
CREATE PROCEDURE [dbo].[SP_GetCAMSUploadRejectsTransaction]  
 -- Add the parameters for the stored procedure here  
 @processId INT,
 @currentPage INT
 
AS 
	SET NOCOUNT ON  
 
	IF(@CurrentPage IS NULL)   
	BEGIN
		
		select 
			CTS.CMCXTS_Id CAMSTransactionId
			,CTS.ADUL_ProcessId ProcessID
			,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName
			,case when CTS.CMCXTS_IsFolioNew = 0  
				then ''Y''  
				else ''N'' end as FolioExists
			,CTS.CMCXTS_FolioNum Folio
			,CTS.CMCXTS_TransactionNum TransactionNumber
			,case when CTS.CMCXTS_IsRejected = 1  
				then ''Y''  
				else ''N'' end as IsRejected  
		FROM
			CustomerMFCAMSXtrnlTransactionStaging CTS  
			left outer join Customer CUST 
			ON CUST.C_CustomerId = CTS.C_CustomerId  
		WHERE
		CTS.ADUL_ProcessId = @processId  
		
	END
	
	ELSE IF(@CurrentPage IS NOT NULL)
	BEGIN
		
		DECLARE @intStartRow int;   
		DECLARE @intEndRow int;  
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;    
		SET @intEndRow = @CurrentPage * 10;
		
		WITH Entries AS  
		(  
			 SELECT
				ROW_NUMBER() over ( ORDER BY CTS.ADUL_ProcessId ASC ) as RowNum 
				,CTS.CMCXTS_Id CAMSTransactionId
				,CTS.ADUL_ProcessId ProcessID
				,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName
				,case when CTS.CMCXTS_IsFolioNew = 0  
					then ''Y''  
					else ''N'' end as FolioExists
				,CTS.CMCXTS_FolioNum Folio
				,CTS.CMCXTS_TransactionNum TransactionNumber
				,case when CTS.CMCXTS_IsRejected = 1  
					then ''Y''  
					else ''N'' end as IsRejected  
			FROM
				CustomerMFCAMSXtrnlTransactionStaging CTS  
				left outer join Customer CUST 
				ON CUST.C_CustomerId = CTS.C_CustomerId  
			WHERE
				CTS.ADUL_ProcessId = @processId    
		)  
		
		SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT 
		FROM
			CustomerMFCAMSXtrnlTransactionStaging CTS  
			left outer join Customer CUST 
			ON CUST.C_CustomerId = CTS.C_CustomerId  
		WHERE
			CTS.ADUL_ProcessId = @processId 
		
	END 
 
	SET NOCOUNT OFF  
  ' 
END
GO
