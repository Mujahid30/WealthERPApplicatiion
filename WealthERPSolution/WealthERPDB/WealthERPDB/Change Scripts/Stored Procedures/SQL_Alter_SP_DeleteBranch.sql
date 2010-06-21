-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_DeleteBranch

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

-- exec SP_DeleteBranch 1015 