/****** Object:  StoredProcedure [dbo].[SP_GetKarvyUploadRejectsProfile]    Script Date: 06/24/2009 12:03:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetKarvyUploadRejectsProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetKarvyUploadRejectsProfile]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetKarvyUploadRejectsProfile]    Script Date: 06/24/2009 12:03:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetKarvyUploadRejectsProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
  
  
CREATE PROCEDURE [dbo].[SP_GetKarvyUploadRejectsProfile]  
 -- Add the parameters for the stored procedure here  
@processId INT,
@currentPage INT
   
AS  
 SET NOCOUNT ON  
 
	IF(@CurrentPage IS NULL)   
	BEGIN
	
		 SELECT 
			KPS.CMFKXPS_Id KarvyStagingId
			,KPS.ADUL_ProcessId ProcessID
			,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName   
			,case when KPS.CMFKXPS_IsCustomerNew = 0  
				then ''Y''  
				else ''N'' end as CustomerExists
			,KPS.CMFKXPS_InvestorName NAME
			,KPS.CMFKXPS_PANNumber PanNumber
			,KPS.CMFKXPS_Folio Folio  
			,case when KPS.CMFKXPS_IsRejected = 1  
				then ''Y''  
				else ''N'' end as IsRejected
			,KPS.CMFKXPS_Address#1 + KPS.CMFKXPS_Address#2 + KPS.CMFKXPS_Address#2 as CUSTADDRESS
			,KPS.CMFKXPS_Pincode  
		  FROM 
			CustomerMFKarvyXtrnlProfileStaging KPS  
			left outer join Customer CUST
			ON CUST.C_CustomerId = KPS.C_CustomerId  
		 WHERE
			KPS.ADUL_ProcessId = @processId
		
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
				ROW_NUMBER() over ( ORDER BY KPS.ADUL_ProcessId ASC ) as RowNum 
				,KPS.CMFKXPS_Id KarvyStagingId
				,KPS.ADUL_ProcessId ProcessID
				,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName   
				,case when KPS.CMFKXPS_IsCustomerNew = 0  
					then ''Y''  
					else ''N'' end as CustomerExists
				,KPS.CMFKXPS_InvestorName NAME
				,KPS.CMFKXPS_PANNumber PanNumber
				,KPS.CMFKXPS_Folio Folio  
				,case when KPS.CMFKXPS_IsRejected = 1  
					then ''Y''  
					else ''N'' end as IsRejected
				,KPS.CMFKXPS_Address#1 + KPS.CMFKXPS_Address#2 + KPS.CMFKXPS_Address#2 as CUSTADDRESS
				,KPS.CMFKXPS_Pincode  
			FROM 
				CustomerMFKarvyXtrnlProfileStaging KPS  
				left outer join Customer CUST
				ON CUST.C_CustomerId = KPS.C_CustomerId  
			 WHERE
				KPS.ADUL_ProcessId = @processId
		)  
		
		SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT 
		FROM 
			CustomerMFKarvyXtrnlProfileStaging KPS  
			left outer join Customer CUST
			ON CUST.C_CustomerId = KPS.C_CustomerId  
		WHERE
			KPS.ADUL_ProcessId = @processId
		
	END
   
 SET NOCOUNT OFF  
 ' 
END
GO
