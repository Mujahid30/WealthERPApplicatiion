/****** Object:  StoredProcedure [dbo].[SP_CreateMoneyBackEpisode]    Script Date: 06/24/2009 11:13:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateMoneyBackEpisode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateMoneyBackEpisode]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateMoneyBackEpisode]    Script Date: 06/24/2009 11:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateMoneyBackEpisode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_CreateMoneyBackEpisode]

@CIMBE_RepaymentDate	DATETIME,
@CIMBE_RepaidPer	numeric(5, 2),
@CINP_InsuranceNPId INT,
@CIMBE_CreatedBy	INT,
@CIMBE_ModifiedBy	INT

AS

INSERT INTO CustomerInsuranceMoneyBackEpisodes
(
	CIMBE_RepaymentDate,
	CIMBE_RepaidPer,
	CINP_InsuranceNPId,
	CIMBE_CreatedBy,
	CIMBE_ModifiedBy,
	CIMBE_CreatedOn,
	CIMBE_ModifiedOn
)
VALUES (
	@CIMBE_RepaymentDate,
	@CIMBE_RepaidPer,
	@CINP_InsuranceNPId,
	@CIMBE_CreatedBy,
	@CIMBE_ModifiedBy,
	CURRENT_TIMESTAMP,
	CURRENT_TIMESTAMP
)' 
END
GO
