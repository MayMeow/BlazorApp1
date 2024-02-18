export function initialize() {
    let appIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    appIndexedDb.onupgradeneeded = function () {
        let db = appIndexedDb.result;
        db.createObjectStore("configuration", { keyPath: "id" });
    }
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "Omglol Db"