/****** Object:  StoredProcedure [dbo].[SP_DeleteRMBranchAssociation]    Script Date: 06/24/2009 11:15:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteRMBranchAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteRMBranchAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRMBranchAssociation]    Script Date: 06/24/2009 11:15:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteRMBranchAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_DeleteRMBranchAssociation

@AR_RMId INT,
@AB_BranchId INT

AS

DELETE FROM dbo.AdviserRMBranch WHERE AB_BranchId=@AB_BranchId AND AR_RMId=@AR_RMId' 
END
GO
