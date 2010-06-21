-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetRMBranchAssociation]
@A_AdviserId INT ,
@AR_RMId INT,
@Flag CHAR(1)




AS

BEGIN
	-- Flag is to identify the association A - Associated
	-- N - Not associatied
	
	
	IF(@Flag='A')
	BEGIN
SELECT 
	*
FROM
    dbo.AdviserBranch 
INNER JOIN   dbo.AdviserRMBranch
	ON dbo.AdviserBranch.AB_BranchId = dbo.AdviserRMBranch.AB_BranchId
WHERE 
	AR_RMId=@AR_RMId

    


		
	END

ELSE IF(@Flag='N'	)
BEGIN
	SELECT * FROM [dbo].[AdviserBranch] WHERE [dbo].[AdviserBranch].[A_AdviserId]=@A_AdviserId AND  [dbo].[AdviserBranch].[AB_BranchId] NOT IN (SELECT [dbo].[AdviserRMBranch].[AB_BranchId] FROM [dbo].[AdviserRMBranch] where [dbo].[AdviserRMBranch].[AR_RMId]=@AR_RMId)
END
END

--EXEC [dbo].[SP_GetRMBranchAssociation] 1004,1037,'N'
--	@A_AdviserId = 0, -- INT
--	@AR_RMId = 0, -- INT
--	@Flag = '' -- CHAR(1)
 