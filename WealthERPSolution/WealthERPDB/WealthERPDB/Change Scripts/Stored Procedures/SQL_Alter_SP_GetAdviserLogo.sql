-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetAdviserLogo]
@A_AdviserId int
as
SELECT A_ADVISERLOGO FROM dbo.Adviser WHERE A_ADVISERID=@A_AdviserId
 