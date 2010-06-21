-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetBranchAssociation]

@U_UserId INT


AS

SELECT    
	 dbo.AdviserRMBranch.AB_BranchId AS branchId,
	 dbo.AdviserRMBranch.AR_RMId AS RMId,
	 dbo.AdviserRM.AR_FirstName AS FirstName,
	 dbo.AdviserBranch.AB_BranchCode AS BranchCode,
	 dbo.AdviserBranch.AB_BranchName AS BranchName,
	 dbo.[User].U_UserId AS UserId,
	 dbo.AdviserRM.AR_LastName AS LastName,
	 dbo.AdviserRM.AR_MiddleName AS MiddleName
FROM 
     dbo.AdviserRMBranch 
INNER JOIN
     dbo.AdviserBranch ON dbo.AdviserRMBranch.AB_BranchId = dbo.AdviserBranch.AB_BranchId 
INNER JOIN
     dbo.AdviserRM ON dbo.AdviserRMBranch.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN
     dbo.[User] ON dbo.AdviserRMBranch.ARB_CreatedBy = dbo.[User].U_UserId
       
      WHERE dbo.[User].U_UserId =@U_UserId 
 --      EXEC SP_GetBranchAssociation 1665
	--@U_UserId = 0 -- INT
 