-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetBranchTerminals

@AB_BranchId INT

AS

SELECT * FROM dbo.AdviserTerminal WHERE AB_BranchId=@AB_BranchId 