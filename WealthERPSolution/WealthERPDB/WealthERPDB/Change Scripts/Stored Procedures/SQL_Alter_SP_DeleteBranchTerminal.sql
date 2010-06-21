-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_DeleteBranchTerminal

@AT_Id INT

AS

DELETE FROM dbo.AdviserTerminal WHERE AT_Id=@AT_Id
 