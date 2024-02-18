export function initialize() {
    let appIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    appIndexedDb.onupgradeneeded = function () {
        let db = appIndexedDb.result;
        db.createObjectStore("configuration", { keyPath: "id" });
    }
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "Omglol Db"

export function put(collectionName, value)
{
    let blazorOmgLolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    blazorOmgLolIndexedDb.onsuccess = function () {
        let transaction = blazorOmgLolIndexedDb.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function pull(collectionName, id)
{
    let request = new Promise((resolve) => {
        let blazorOmgLolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        blazorOmgLolIndexedDb.onsuccess = function () {
            let transaction = blazorOmgLolIndexedDb.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    let result = await request;

    return result;
}

export async function hasKey(collectionName, id) {
    let request = new Promise((resolve) => {
        let blazorOmgLolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        blazorOmgLolIndexedDb.onsuccess = function () {
            let transaction = blazorOmgLolIndexedDb.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e) {
                if (result.result) {
                    resolve(true)
                } else {
                    resolve(false)
                }
            }
        }
    });

    let result = await request;

    return result;
}