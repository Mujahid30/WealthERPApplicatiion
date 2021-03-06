/****** Object:  StoredProcedure [dbo].[SP_DeleteBranch]    Script Date: 06/24/2009 11:14:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteBranch]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBranch]    Script Date: 06/24/2009 11:14:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteBranch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_DeleteBranch

@AB_BranchId INT

AS

BEGIN
	
	BEGIN TRANSACTION


DELETE 
	
FROM 
	dbo.AdviserTerminal
WHERE  EXISTS 
(
	SELECT @AB_BranchId FROM dbo.AdviserTerminal WHERE dbo.AdviserTerminal.AB_BranchId =@AB_BranchId
)


DELETE 
	
FROM 
	dbo.AdviserRMBranch
WHERE EXISTS 
(
	SELECT @AB_BranchId FROM dbo.AdviserRMBranch WHERE dbo.AdviserRMBranch.AB_BranchId=@AB_BranchId
)
	
	


	
DELETE 
	
FROM 
	dbo.AdviserBranch
WHERE 
	dbo.AdviserBranch.AB_BranchId=@AB_BranchId
	


IF (@@ERROR<>0)
	ROLLBACK TRANSACTION;

COMMIT TRANSACTION
	
	
	
END

-- exec SP_DeleteBranch 1015' 
END
GO
