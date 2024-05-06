import React from 'react'
import Navbar from '../navbar/Navbar'
import { Outlet } from 'react-router'

const UserLayout = () => {
  return (
    <div>
        <Navbar/>

        <div className=''>
            <Outlet/>
        </div>
    </div>
  )
}

export default UserLayout