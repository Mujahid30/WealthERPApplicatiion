-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetSchemeCode]
@PASP_SchemePlan VARCHAR(MAX)
AS
SELECT * FROM dbo.ProductAMCSchemePlan WHERE PASP_SchemePlanName=@PASP_SchemePlan 