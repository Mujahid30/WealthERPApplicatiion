/****** Object:  StoredProcedure [dbo].[SP_DeleteRMDetails]    Script Date: 06/24/2009 11:15:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteRMDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteRMDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRMDetails]    Script Date: 06/24/2009 11:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteRMDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_DeleteRMDetails

@AR_RMId int


AS
DELETE FROM dbo.AdviserRM WHERE AR_RMId=@AR_RMId
' 
END
GO
