import {useState,useEffect} from 'react'
import {getAllEvents}from '../services/ClientService'
import { useUserContext } from '../context/UserContext';
import Event from '../components/Event';
export default function EventPage(){
    const [events,setEvents]=useState(null);
    const {user}=useUserContext();
    useEffect(()=>{
       getAllEvents(user.id)
       .then((res)=>{
        console.log(res);
        setEvents(res);
       })
    },[]);
    return(<>
    
    <section className="py-5">
    <h1 className="headline"> Events</h1>
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {events===null?<h1>Loading...</h1>:events.map(e=><Event 
                    BirthDayBoyName={e.birthdayBoyName}
                    StartDate={e.startDate}
                    EndDate={e.endDate}
                    Id={e.eventId}
                    key={e.eventId}/>)}
                   
                   
                </div>
            </div>
        </section>
    </>)
}