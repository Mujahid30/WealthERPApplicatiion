/****** Object:  StoredProcedure [dbo].[SP_DeleteBranchDetails]    Script Date: 06/24/2009 11:14:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteBranchDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteBranchDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBranchDetails]    Script Date: 06/24/2009 11:14:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteBranchDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteBranchDetails]

@AB_BranchId INT


AS

BEGIN
	    DECLARE @branchName VARCHAR(10);
	    DECLARE @ErrorMessage VARCHAR(1000);

	    BEGIN TRY --Start the Try Block..
				SELECT @branchName=AB_BranchCode  FROM dbo.AdviserBranch WHERE AB_BranchId=@AB_BranchId

			BEGIN TRANSACTION -- Start the transaction..
        
					
				DELETE FROM dbo.AdviserBranch WHERE AB_BranchId=@AB_BranchId

			COMMIT TRAN -- Transaction Success!

		END TRY

		BEGIN CATCH
		
			ROLLBACK TRAN
				
				SELECT @ErrorMessage = ERROR_MESSAGE();
				RAISERROR(@ErrorMessage, 16, 1)
				RAISERROR(''YOU CAN NOT DELETE BECAUSE  %s BRANCH HAS SOME DEPENDENT DATA ... ''	,16,1,@branchName);

		END CATCH	
	
	
END


--EXEC dbo.SP_DeleteRM 1037, 1665
	
' 
END
GO
