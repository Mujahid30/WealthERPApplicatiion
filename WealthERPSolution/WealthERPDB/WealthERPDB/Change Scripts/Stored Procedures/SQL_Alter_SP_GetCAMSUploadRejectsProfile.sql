  
  
ALTER PROCEDURE [dbo].[SP_GetCAMSUploadRejectsProfile]  
 -- Add the parameters for the stored procedure here  
	 @processId INT,
	 @currentPage INT 
   
AS  
	SET NOCOUNT ON  
 
	IF(@CurrentPage IS NULL)   
	BEGIN   
		select  
			 CPS.CMGCXPS_Id CAMSProfileStagingId  
			 ,CPS.ADUL_ProcessId ProcessID  
			 ,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName  
			 ,case when CPS.CMGCXPS_IsCustomerNew = 0  
				then 'Y'  
				else 'N' end as CustomerExists  
			 ,CPS.CMGCXPS_INV_NAME NAME  
			 ,CPS.CMGCXPS_PAN_NO PANNumber  
			 ,CPS.CMGCXPS_FOLIOCHK Folio  
			 ,CPS.CMGCXPS_SCH_NAME Scheme   
			 ,case when CPS.CMGCXPS_IsRejected = 1  
				 then 'Y'  
				 else 'N' end as IsRejected  
			 ,CPS.CMGCXPS_ADDRESS1 + CPS.CMGCXPS_ADDRESS2 + CPS.CMGCXPS_ADDRESS3 as CUSTADDRESS  
			 ,CPS.CMGCXPS_PINCODE Pincode  
		 from   
			 CustomerMFCAMSXtrnlProfileStaging CPS  
			 left outer JOIN Customer CUST   
			 ON CUST.C_CustomerId = CPS.C_CustomerId  
		 where   
			CPS.ADUL_ProcessId = @processId  
	END
	
	ELSE IF(@CurrentPage IS NOT NULL)  
	BEGIN  
		
		DECLARE @intStartRow int;   
		DECLARE @intEndRow int;  
		SET @intStartRow = (@CurrentPage -1) * 10 + 1;    
		SET @intEndRow = @CurrentPage * 10;
		 
		WITH Entries AS  
		(  
		 select 
			ROW_NUMBER() over ( ORDER BY  
			   CUST.C_FirstName + CUST.C_LastName ASC ) as RowNum 
			,CPS.CMGCXPS_Id CAMSProfileStagingId  
			,CPS.ADUL_ProcessId ProcessID  
			,CUST.C_FirstName + CUST.C_LastName as WERPCUstomerName  
			,case when CPS.CMGCXPS_IsCustomerNew = 0  
				then 'Y'  
				else 'N' end as CustomerExists  
			,CPS.CMGCXPS_INV_NAME NAME  
			,CPS.CMGCXPS_PAN_NO PANNumber  
			,CPS.CMGCXPS_FOLIOCHK Folio  
			,CPS.CMGCXPS_SCH_NAME Scheme   
			,case when CPS.CMGCXPS_IsRejected = 1  
				 then 'Y'  
				 else 'N' end as IsRejected  
			,CPS.CMGCXPS_ADDRESS1 + CPS.CMGCXPS_ADDRESS2 + CPS.CMGCXPS_ADDRESS3 as CUSTADDRESS  
			,CPS.CMGCXPS_PINCODE Pincode  
		    
		 from   
			CustomerMFCAMSXtrnlProfileStaging CPS  
			left outer JOIN Customer CUST   
			ON CUST.C_CustomerId = CPS.C_CustomerId
		 where   
			CPS.ADUL_ProcessId = @processId    
		)  
		
		Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow  
		   
		SELECT COUNT(*) AS CNT from   
			CustomerMFCAMSXtrnlProfileStaging CPS  
			left outer JOIN Customer CUST   
			ON CUST.C_CustomerId = CPS.C_CustomerId
		 where   
			CPS.ADUL_ProcessId = @processId  
		   
	END  
 
 SET NOCOUNT OFF   