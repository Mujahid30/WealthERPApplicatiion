/****** Object:  StoredProcedure [dbo].[SP_UpdateCAMSUploadProfile]    Script Date: 06/24/2009 12:06:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCAMSUploadProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCAMSUploadProfile]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCAMSUploadProfile]    Script Date: 06/24/2009 12:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCAMSUploadProfile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_UpdateCAMSUploadProfile]
	-- Add the parameters for the stored procedure here
	@CMGCXPS_Id int,
	@PanNumber Varchar(20) = NULL,
	@Folio Varchar(20) = NULL
AS
	SET NOCOUNT ON
	
	Declare  
	  @bTran AS INT,      
	  @lErrCode AS INT

	-- Begin Tran
	If (@@Trancount = 0)  
	 Begin  
	  Set @bTran = 1  
	  Begin Transaction  
	 End  
	 
	 IF (@PanNumber IS NOT NULL AND @Folio IS NOT NULL)
	 BEGIN
	 	UPDATE CustomerMFCAMSXtrnlProfileStaging 
		SET 
			CMGCXPS_FOLIOCHK = @Folio,
			CMGCXPS_PAN_NO = @PanNumber
		WHERE 
			CMGCXPS_Id = @CMGCXPS_Id
	 END
	 
	 ELSE IF (@PanNumber IS NOT NULL AND @Folio IS NULL)
	 BEGIN
	 	
	 	UPDATE CustomerMFCAMSXtrnlProfileStaging 
		SET 
			CMGCXPS_PAN_NO = @PanNumber
		WHERE 
			CMGCXPS_Id = @CMGCXPS_Id
	 	
	 END
	
	 ELSE IF (@PanNumber IS NULL AND @Folio IS NOT NULL)
	 BEGIN
	 	
	 	UPDATE CustomerMFCAMSXtrnlProfileStaging 
		SET 
			CMGCXPS_FOLIOCHK = @Folio
		WHERE 
			CMGCXPS_Id = @CMGCXPS_Id
	 	
	 END
	
	
		Success:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin                                    
  Commit Tran      
 End      
 Return 0      
      
 Goto Done      
      
Error:      
 If (@bTran = 1 And @@Trancount > 0)      
 Begin      
  Rollback Transaction      
 End      
 Return @lErrCode      

	       
Done:     
SET NOCOUNT OFF       
' 
END
GO
