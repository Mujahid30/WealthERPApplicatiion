/****** Object:  StoredProcedure [dbo].[SP_GetAdviser]    Script Date: 06/24/2009 11:57:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviser]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviser]    Script Date: 06/24/2009 11:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetAdviser]
@A_AdviserId BIGINT
as
select * from dbo.Adviser where A_AdviserId=@A_AdviserId
' 
END
GO
