const baseUrl="https://localhost:7194/"

export const logIn =async (username,password)=>{
const response= await fetch(`${baseUrl}login?UserName=${username}&Password=${password}`,{
    method:'POST'
});
return await response.json();
}
export const getAllEmployees=async()=>{
    const response=await fetch(`${baseUrl}getAll`,{
        method:'GET'
    });
 return await response.json();
}
export const startEvent=async(startDate,initiatorId,birthdayBoyId)=>{
    const response=await fetch(`${baseUrl}startEvent?StartDate=${startDate}&InitiatorId=${initiatorId}&BirthdayBoyId=${birthdayBoyId}`,{
        method:'POST'
    });
    return await response.json();
}
export const getAllEvents=async(id)=>{
    const response=await fetch(`${baseUrl}getAllEvents?id=${id}`,{
        method:'GET'
    });
    return await response.json();
}
export const getAllGifts=async ()=>{
    const response=await fetch(`${baseUrl}getAllGifts`,{
        method:'GET'
    });
    return await response.json();
}
export const vote=async (eventId,giftId,voterId)=>{
    const response=await fetch(`${baseUrl}vote?EventId=${eventId}&GiftId=${giftId}&VoterId=${voterId}`,{
        method:'POST'
    });
    return await response.json();
}
export const getResult=async (eventId,personId)=>{
    const response=await fetch(`${baseUrl}getResult?EventId=${eventId}&PersonId=${personId}`,{
        method:'GET'
    });
    return await response.json();

}
export const trackVoting=async (eventId,personId)=>{
    const response=await fetch(`${baseUrl}trackVoting?EventId=${eventId}&PersonId=${personId}`,{
        method:'GET'
    });
    return await response.json();
}