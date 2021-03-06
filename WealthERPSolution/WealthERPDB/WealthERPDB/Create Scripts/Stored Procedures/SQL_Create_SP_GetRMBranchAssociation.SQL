/****** Object:  StoredProcedure [dbo].[SP_GetRMBranchAssociation]    Script Date: 06/24/2009 12:04:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMBranchAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMBranchAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMBranchAssociation]    Script Date: 06/24/2009 12:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMBranchAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetRMBranchAssociation]
@A_AdviserId INT ,
@AR_RMId INT,
@Flag CHAR(1)




AS

BEGIN
	-- Flag is to identify the association A - Associated
	-- N - Not associatied
	
	
	IF(@Flag=''A'')
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

ELSE IF(@Flag=''N''	)
BEGIN
	SELECT * FROM [dbo].[AdviserBranch] WHERE [dbo].[AdviserBranch].[A_AdviserId]=@A_AdviserId AND  [dbo].[AdviserBranch].[AB_BranchId] NOT IN (SELECT [dbo].[AdviserRMBranch].[AB_BranchId] FROM [dbo].[AdviserRMBranch] where [dbo].[AdviserRMBranch].[AR_RMId]=@AR_RMId)
END
END

--EXEC [dbo].[SP_GetRMBranchAssociation] 1004,1037,''N''
--	@A_AdviserId = 0, -- INT
--	@AR_RMId = 0, -- INT
--	@Flag = '''' -- CHAR(1)
' 
END
GO
