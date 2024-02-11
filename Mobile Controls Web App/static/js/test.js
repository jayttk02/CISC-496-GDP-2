
async function testCall(){
    var response = await httpPost("/new");
}

async function httpPost(url, params={"test": "test"}){
    const options = {
        method: "POST",
        headers: {
           "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
    };
    const response = await fetch(url, options);
    return response;
    // const json = await response;//.json();
    // return json;
}