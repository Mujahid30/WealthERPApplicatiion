/****** Object:  StoredProcedure [dbo].[SP_GetAdviserStaffDetails]    Script Date: 06/24/2009 11:57:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserStaffDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserStaffDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserStaffDetails]    Script Date: 06/24/2009 11:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserStaffDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetAdviserStaffDetails]
@U_UserId int
as
select * from AdviserRM where U_UserId=@U_UserId' 
END
GO
