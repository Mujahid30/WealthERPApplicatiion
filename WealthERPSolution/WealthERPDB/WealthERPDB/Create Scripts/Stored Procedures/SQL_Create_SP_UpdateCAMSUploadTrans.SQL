/****** Object:  StoredProcedure [dbo].[SP_UpdateCAMSUploadTrans]    Script Date: 06/24/2009 12:06:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCAMSUploadTrans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateCAMSUploadTrans]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCAMSUploadTrans]    Script Date: 06/24/2009 12:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateCAMSUploadTrans]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[SP_UpdateCAMSUploadTrans]
	-- Add the parameters for the stored procedure here
	@CMCXTS_Id int,
	@TransactionNumber Varchar(20),
	@Folio Varchar(20)
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
	 
	Update dbo.CustomerMFCAMSXtrnlTransactionStaging set
	CMCXTS_FolioNum = @Folio,
	CMCXTS_TransactionNum = @TransactionNumber
	where CMCXTS_Id = @CMCXTS_Id
	
	
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
