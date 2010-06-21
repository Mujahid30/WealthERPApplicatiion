-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetRMLogo]
@AR_RMID BIGINT
as
SELECT A.A_ADVISERLOGO FROM dbo.Adviser A,dbo.AdviserRM R WHERE A.A_ADVISERID=R.A_ADVISERID AND R.AR_RMID=@AR_RMID

 