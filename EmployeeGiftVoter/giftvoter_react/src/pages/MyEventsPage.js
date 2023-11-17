import {useState,useEffect} from 'react'
import {getMyEvents}from '../services/ClientService'
import { useUserContext } from '../context/UserContext';
import CloseEvent from '../components/CloseEvent'
export default function MyEventsPage(){
    const[myEvents,setMyEvents]=useState(null);
    const{user}=useUserContext();
    useEffect(()=>{
     getMyEvents(user.id)
     .then((res)=>{
       console.log(res);
       setMyEvents(res);
     });
    },[]);
    return (<>
     <section className="py-5">
     <h1 className="headline">My Events</h1>
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {myEvents===null?<h1>Loading...</h1>:myEvents.map(e=><CloseEvent 
                    EventId={e.eventId}
                    StartDate={e.startDate}
                    InitiatorId={e.initiatorId}
                    BirthdayBoyId={e.birthdayBoyId}
                    BirthdayBoyName={e.birthdayBoyName}
                    EndDate={e.endDate}
                    key={e.eventId}/>)}
                   
                   
                </div>
            </div>
        </section>
    </>)
}