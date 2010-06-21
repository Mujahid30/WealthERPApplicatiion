ALTER PROCEDURE [dbo].[SP_GetWERPUploadRejectsProfile]
@processId INT,
@currentPage INT

AS

SET NOCOUNT ON
	
	IF (@currentPage IS NULL)
	BEGIN
		
		select  
			 CPS.CMFXPS_Id WERPProfileStagingId  
			 ,CPS.ADUL_ProcessId ProcessID  
			 ,CUST.C_FirstName + CUST.C_LastName as WERPCustomerName  
			 ,case when CPS.CMFXPS_IsCustomerNew = 0  
				then 'Y'  
				else 'N' end as CustomerExists  
			 ,CPS.CMFXPS_FirstName + ' ' + CMFXPS_MiddleName + ' ' + CPS.CMFXPS_LastName AS NAME  
			 ,CPS.CMFXPS_PANNum AS PANNumber  
			 ,CPS.CMFXPS_FolioNum AS Folio  
			 ,P.PA_AMCName AS Scheme   
			 ,case when CPS.CMFXPS_IsRejected = 1  
				 then 'Y'  
				 else 'N' end as IsRejected  
			 ,CPS.CMFXPS_Adr1Line1 + CPS.CMFXPS_Adr1Line2 + CPS.CMFXPS_Adr1Line3 AS CUSTADDRESS  
			 ,CPS.CMFXPS_Adr1PinCode AS Pincode  
		 from   
			 CustomerMFXtrnlProfileStaging CPS  
			 left outer JOIN Customer CUST   
			 ON CUST.C_CustomerId = CPS.C_CustomerId 
			 INNER JOIN
			 dbo.ProductAMC AS P
			 ON CPS.PA_AMCCode = P.PA_AMCCode
		 where   
			CPS.ADUL_ProcessId = @processId 
		
	END	
	ELSE IF (@currentPage IS NOT NULL)
	BEGIN
		
		DECLARE @intStartRow INT;   
		DECLARE @intEndRow INT;  
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;    
		SET @intEndRow = @CurrentPage * 10;
		
		WITH Entries AS  
		(  
		 select 
			ROW_NUMBER() over ( ORDER BY  CPS.ADUL_ProcessId ASC ) as RowNum 
			,CPS.CMFXPS_Id WERPProfileStagingId  
			 ,CPS.ADUL_ProcessId ProcessID  
			 ,CUST.C_FirstName + CUST.C_LastName as WERPCustomerName  
			 ,case when CPS.CMFXPS_IsCustomerNew = 0  
				then 'Y'  
				else 'N' end as CustomerExists  
			 ,CPS.CMFXPS_FirstName + ' ' + CMFXPS_MiddleName + ' ' + CPS.CMFXPS_LastName AS NAME  
			 ,CPS.CMFXPS_PANNum PANNumber  
			 ,CPS.CMFXPS_FolioNum AS Folio  
			 ,P.PA_AMCName AS Scheme   
			 ,case when CPS.CMFXPS_IsRejected = 1  
				 then 'Y'  
				 else 'N' end as IsRejected  
			 ,CPS.CMFXPS_Adr1Line1 + CPS.CMFXPS_Adr1Line2 + CPS.CMFXPS_Adr1Line3 AS CUSTADDRESS  
			 ,CPS.CMFXPS_Adr1PinCode AS Pincode  
		 from   
			 CustomerMFXtrnlProfileStaging CPS  
			 left outer JOIN Customer CUST   
			 ON CUST.C_CustomerId = CPS.C_CustomerId 
			 INNER JOIN
			 dbo.ProductAMC AS P
			 ON CPS.PA_AMCCode = P.PA_AMCCode
		 where   
			CPS.ADUL_ProcessId = @processId     
		)  
		
		SELECT * FROM Entries WHERE RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT FROM   
			CustomerMFXtrnlProfileStaging CPS  
			 left outer JOIN Customer CUST   
			 ON CUST.C_CustomerId = CPS.C_CustomerId 
			 INNER JOIN
			 dbo.ProductAMC AS P
			 ON CPS.PA_AMCCode = P.PA_AMCCode
		 WHERE   
			CPS.ADUL_ProcessId = @processId  
		
	END
	
SET NOCOUNT OFF
 