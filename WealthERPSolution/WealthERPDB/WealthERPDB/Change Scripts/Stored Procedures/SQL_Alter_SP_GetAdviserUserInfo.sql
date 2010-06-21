-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAdviserUserInfo]
@A_AdviserId BIGINT
as
select * from [User] INNER JOIN dbo.Adviser ON [User].U_UserId=dbo.Adviser.U_UserId where dbo.Adviser.A_AdviserId=@A_AdviserId
 