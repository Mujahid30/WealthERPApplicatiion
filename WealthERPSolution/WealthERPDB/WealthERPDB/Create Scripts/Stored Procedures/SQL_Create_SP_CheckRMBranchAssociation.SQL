/****** Object:  StoredProcedure [dbo].[SP_CheckRMBranchAssociation]    Script Date: 06/24/2009 11:03:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CheckRMBranchAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CheckRMBranchAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckRMBranchAssociation]    Script Date: 06/24/2009 11:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CheckRMBranchAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_CheckRMBranchAssociation

@AR_RMId INT,
@AB_BranchId INT

AS

SELECT COUNT(*) FROM dbo.AdviserRMBranch WHERE AR_RMId=@AR_RMId AND AB_BranchId=@AB_BranchId

--EXEC SP_CheckRMBranchAssociation 1053,1022
--	@AR_RMId = 0, -- INT
--	@AB_BranchId = 0 -- INT
' 
END
GO
