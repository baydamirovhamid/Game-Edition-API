﻿CREATE TRIGGER GAME_TG
BEFORE
INSERT ON "game"
FOR EACH ROW EXECUTE PROCEDURE GAME_TG_FUNC();

CREATE TRIGGER COMPANY_TG
BEFORE
INSERT ON "company"
FOR EACH ROW EXECUTE PROCEDURE COMPANY_TG_FUNC();

CREATE TRIGGER PLATFORM_TG
BEFORE
INSERT ON "platform"
FOR EACH ROW EXECUTE PROCEDURE PLATFORM_TG_FUNC();

CREATE TRIGGER GAME_COMPANY_TG
BEFORE
INSERT ON "game_company"
FOR EACH ROW EXECUTE PROCEDURE GAME_COMPANY_TG_FUNC();

CREATE TRIGGER GAME_PLATFORM_TG
BEFORE
INSERT ON "game_platform"
FOR EACH ROW EXECUTE PROCEDURE GAME_PLATFORM_TG_FUNC();

CREATE TRIGGER STATIC_DATA_TG
BEFORE
INSERT ON "static_data"
FOR EACH ROW EXECUTE PROCEDURE STATIC_DATA_TG_FUNC();


CREATE TRIGGER BASKET_TG
BEFORE
INSERT ON "baskter"
FOR EACH ROW EXECUTE PROCEDURE BASKET_TG_FUNC();

CREATE TRIGGER CUSTOMER_TG
BEFORE
INSERT ON "customer"
FOR EACH ROW EXECUTE PROCEDURE CUSTOMER_TG_FUNC();

