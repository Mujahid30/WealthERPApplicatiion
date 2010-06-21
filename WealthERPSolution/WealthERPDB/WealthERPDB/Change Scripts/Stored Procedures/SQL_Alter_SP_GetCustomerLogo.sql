-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER  procedure [dbo].[SP_GetCustomerLogo]
@C_CUSTOMERID BIGINT
as
SELECT A.A_ADVISERLOGO FROM dbo.Adviser A,dbo.AdviserRM R,dbo.Customer C WHERE A.A_ADVISERID=R.A_ADVISERID AND R.AR_RMID=C.AR_RMID AND C.C_CUSTOMERID=@C_CUSTOMERID
 